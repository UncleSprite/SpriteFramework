using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Entity;
using Sprite.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public class DbContextBase : DbContext, IDbContext
    {
        protected readonly IServiceProvider _serviceProvider;

        public DbContextBase(DbContextOptions options, IServiceProvider serviceProvider)
       : base(options)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        /// <summary>
        /// 获取或设置 当前上下文所在工作单元，为null将使用EF自动事务而不启用手动事务
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 将此上下文中所作的所有更改，保存到数据库中
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>
        /// 将此上下文中所作的所有更改，异步保存到数据库中
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 创建上下文数据模型时，对各个实体类数据库映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEntityConfigurationTypeFinder entityConfigurationTypeFinder = _serviceProvider.GetService<IEntityConfigurationTypeFinder>();
            Type[] types = entityConfigurationTypeFinder.FindAll(true);

            if (types.Length == 0)
                return;

            List<IEntityRegister> registers =
                types.Select(type => Activator.CreateInstance(type) as IEntityRegister).ToList();
            foreach (var entityRegister in registers)
            {
                entityRegister.RegisterTo(modelBuilder);
            }
        }
    }
}
