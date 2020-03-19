using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprite.EntityFrameWorkCore
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
        /// 异步提交数据上下文的所有变更
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancelToken = default(CancellationToken));
    }
}
