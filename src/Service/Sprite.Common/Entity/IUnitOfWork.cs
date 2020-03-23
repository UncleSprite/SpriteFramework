using Sprite.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprite.Common.Entity
{
    /// <summary>
    /// 业务单元操作接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 获取  工作单元得事务是否已提交
        /// </summary>
        bool HasCommitted { get; }

        /// <summary>
        /// 获取数据上下文
        /// </summary>
        /// <returns></returns>
        IDbContext GetDbContext();

        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        void BeginOrUseTransaction();
        /// <summary>
        /// 异步开启事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeginOrUseTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 提交当前上下文得事务更改
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚所有事务
        /// </summary>
        void Rollback();
    }
}
