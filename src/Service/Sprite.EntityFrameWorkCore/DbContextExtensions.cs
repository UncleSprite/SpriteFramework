using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sprite.Common.Entity.Base;
using Sprite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 数据上下文扩展方法
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 更新上下文中指定实体的状态
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="context">上下文对象</param>
        /// <param name="entities">要更新的实体类型</param>
        public static void Update<TEntity, TKey>(this DbContext context, params TEntity[] entities)
            where TEntity : class, IEntity<TKey>
        {
            entities.CheckNotNull(nameof(entities));
            DbSet<TEntity> set = context.Set<TEntity>();
            foreach (TEntity entity in entities)
            {
                try
                {
                    EntityEntry<TEntity> entry = context.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        set.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = set.Find(entity.Id);
                    context.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }
    }
}
