using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprite.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 数据实体映射配置基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityTypeConfigurationBase<TEntity, TKey> : IEntityTypeConfiguration<TEntity>, IEntityRegister
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder"></param>
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// 将当前实体类映射对象注册到数据上下文模型构建器中
        /// </summary>
        /// <param name="builder"></param>
        public void RegisterTo(ModelBuilder builder)
        {
          
            builder.ApplyConfiguration(this);
          
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Entity<TEntity>().HasQueryFilter(m => !((ISoftDeletable)m).Deleted);
            }
        }
    }
}
