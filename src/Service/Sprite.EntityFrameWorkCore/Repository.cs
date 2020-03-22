using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Dependency;
using Sprite.Common.Entity;
using Sprite.Common.Entity.Base;
using Sprite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 实体数据存储操作类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>, IScopeDependency
          where TEntity : class, IEntity<TKey>
          where TKey : IEquatable<TKey>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextBase _dbContext;

        /// <summary>
        /// 初始化一个<see cref="Repository{TEntity,TKey}"/>的实例
        /// </summary>
        /// <param name="serviceProvider"></param>
        public Repository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            UnitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            _dbContext = (DbContextBase)UnitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///  获取 <typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> Entities => _dbSet.AsQueryable().AsNoTracking();

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> TrackEntities => _dbSet.AsQueryable().AsTracking();

        #region 同步方法
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            _dbSet.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Delete(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            DeleteINternal(entities);

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(TKey key)
        {
            TEntity entity = _dbSet.Find(key);
            return Delete(entity);
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Update(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            entities = CheckUpdate(entities);

            _dbContext.Update<TEntity, TKey>(entities);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public virtual bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            predicate.CheckNotNull(nameof(predicate));

            TKey defaultId = default(TKey);
            var entity = _dbSet.Where(predicate).Select(m => new { m.Id }).FirstOrDefault();
            bool exists = !typeof(TKey).IsValueType && ReferenceEquals(id, null) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !EntityBase<TKey>.IsKeyEqual(entity.Id, id);
            return exists;
        }

        /// <summary>
        /// 获取指定编号的实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey key)
        {
            return _dbSet.Find(key);
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> Query()
        {
            return Query(null);
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> QueryInclude(params Expression<Func<TEntity, object>>[] includePropertySelectors)
        {
            return TrackQueryInclude(includePropertySelectors).AsNoTracking();
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <param name="predicate">数据过滤表达式</param>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return TrackQuery(predicate).AsNoTracking();
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> TrackQuery()
        {
            return TrackQuery(null);
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> TrackQueryInclude(params Expression<Func<TEntity, object>>[] includePropertySelectors)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (includePropertySelectors == null || includePropertySelectors.Length == 0)
            {
                return query;
            }

            foreach (Expression<Func<TEntity, object>> selector in includePropertySelectors)
            {
                query = query.Include(selector);
            }
            return query;
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源，
        /// </summary>
        /// <param name="predicate">数据过滤表达式</param>
        /// <returns>符合条件的数据集</returns>
        public virtual IQueryable<TEntity> TrackQuery(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (predicate == null)
            {
                return query;
            }
            return query.Where(predicate);
        }

        #endregion

        #region 异步方法
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            await _dbSet.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            DeleteINternal(entities);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TKey key)
        {
            TEntity entity = await _dbSet.FindAsync(key);
            return await DeleteAsync(entity);
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(params TEntity[] entities)
        {
            entities.CheckNotNull(nameof(entities));
            entities = CheckUpdate(entities);

            _dbContext.Update<TEntity, TKey>(entities);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public virtual async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            predicate.CheckNotNull(nameof(predicate));

            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).Select(m => new { m.Id }).FirstOrDefaultAsync();
            bool exists = !typeof(TKey).IsValueType && ReferenceEquals(id, null) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !EntityBase<TKey>.IsKeyEqual(entity.Id, id);
            return exists;
        }

        /// <summary>
        /// 异步查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }
        #endregion

        #region 私有方法
        private void DeleteINternal(params TEntity[] entities)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                foreach (var entity in entities)
                {
                    IDataBase softDeletableEntity = (IDataBase)entity;

                    softDeletableEntity.Deleted = true;
                    softDeletableEntity.DeletedAt = DateTime.Now;
                }
            }
            else
            {
                _dbSet.RemoveRange(entities);
            }
        }


        private TEntity[] CheckUpdate(params TEntity[] entities)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                TEntity entity = entities[i];
                IDataBase entity1 = (IDataBase)entity;
                entity1.UpdatedAt = DateTime.Now;

                entities[i] = (TEntity)entity1;
            }
            return entities;
        }
        #endregion
    }
}
