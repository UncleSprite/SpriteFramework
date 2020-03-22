using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprite.Common.Entity.Base
{
    /// <summary>
    /// 定义数据库上下文接口
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 提交数据上下文变更
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 异步提交数据上下文所有变更
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
