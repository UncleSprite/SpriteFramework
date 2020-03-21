using Sprite.Common.Data;
using Sprite.Common.Extensions;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sprite.Common.Entity.Base
{
    public class EntityBase<TKey> : IEntity<TKey>, IDataBase
          where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityBase{TKey}"/>类型的新实例
        /// </summary>
        protected EntityBase(bool isPre = false)
        {
            if (!isPre)
            {
                if (typeof(TKey) == typeof(Guid))
                {
                    Id = CombGuid.NewGuid().CastTo<TKey>();
                }
                if (typeof(TKey) == typeof(string))
                {
                    Id = CombGuid.NewGuid().CastTo<TKey>();
                }
            }
        }

        /// <summary>
        /// 获取或设置  编号
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        [MaxLength(36)]

        public Guid? CreatedById { get; set; }
        /// <summary>
        /// 更新者Id
        /// </summary>
        [MaxLength(36)]
        public Guid? UpdatedById { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 删除者Id
        /// </summary>
        [MaxLength(36)]
        public Guid? DeletedById { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is EntityBase<TKey> entity))
            {
                return false;
            }

            return IsKeyEqual(entity.Id, Id);
        }
        /// <summary>
        /// 实体Id是否相等
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsKeyEqual(TKey id1, TKey id2)
        {
            if (id1 == null && id2 == null)
            {
                return true;
            }

            if (id1 == null || id2 == null)
            {
                return false;
            }
            Type type = typeof(TKey);
            if (type.IsDeriveClassFrom(typeof(IEquatable<TKey>)))
            {
                return id1.Equals(id2);
            }

            return Equals(id1, id2);
        }
        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。<br/>
        /// 如果<c>Id</c>为<c>null</c>则返回0，
        /// 如果不为<c>null</c>则返回<c>Id</c>对应的哈希值
        /// </returns>
        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.ToString().GetHashCode();
        }
    }
}
