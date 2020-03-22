using Microsoft.EntityFrameworkCore;
using Sprite.Common.Dependency;
using Sprite.Common.Entity;
using Sprite.Common.Entity.Base;
using Sprite.EntityFrameWorkCore.Default;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 业务单元操作
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IScopeDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextBase _dbContext;
        private DbTransaction _dbTransaction;
        private DbConnection _dbConnection;
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContext = (DbContextBase)_serviceProvider.GetService(typeof(DefaultDbContext));
        }

        public bool HasCommitted { get; private set; }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginOrUseTransaction()
        {
            if (_dbContext == null)
            {
                return;
            }

            if (_dbTransaction?.Connection == null)
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }

                _dbTransaction = _dbConnection.BeginTransaction();
            }
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.UseTransaction(_dbTransaction);
            }
            HasCommitted = false;
        }

        /// <summary>
        /// 异步开启事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BeginOrUseTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_dbContext == null)
            {
                return;
            }

            if (_dbTransaction?.Connection == null)
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    await _dbConnection.OpenAsync(cancellationToken);
                }

                _dbTransaction = _dbConnection.BeginTransaction();
            }
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.UseTransaction(_dbTransaction);
            }

            HasCommitted = false;
        }

        /// <summary>
        /// 提交当前上下文的事务提交
        /// </summary>
        public void Commit()
        {
            if (HasCommitted || _dbContext == null || _dbTransaction == null)
            {
                return;
            }
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbContext.Database.CurrentTransaction.Dispose();
            HasCommitted = true;
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _dbTransaction?.Dispose();
            _dbContext?.Dispose();

            _disposed = true;
        }

        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <returns></returns>
        public IDbContext GetDbContext()
        {
            _dbConnection = _dbContext.Database.GetDbConnection();
            _dbContext.UnitOfWork = this;
            return _dbContext;
        }

        public void Rollback()
        {
            if (_dbTransaction?.Connection != null)
            {
                _dbTransaction.Rollback();
            }
            _dbTransaction?.Dispose();
            CleanChanges(_dbContext);
            _dbContext.Database.CurrentTransaction?.Rollback();
            _dbContext.Database.CurrentTransaction?.Dispose();
            HasCommitted = true;
        }

        private static void CleanChanges(DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
