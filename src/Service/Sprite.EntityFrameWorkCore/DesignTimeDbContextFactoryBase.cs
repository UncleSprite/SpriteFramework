using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 设计时数据上下文实例工厂基类，用于执行数据迁移
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DbContext
    {
        /// <summary>
        /// 创建一个数据上下文实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual TDbContext CreateDbContext(string[] args)
        {
            string connString = GetConnectionString();
            if (connString == null)
            {
                return null;
            }
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<TDbContext>();

            if (LazyLoadingProxiesEnabled())
            {
                builder.UseLazyLoadingProxies();
            }

            builder = UseSql(builder, connString);
            //  return new DefaultDbContext(builder.Options,null) as TDbContext;
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), builder.Options, GetServiceProvider());
        }

        /// <summary>
        /// 重写获取数据上下文数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public abstract string GetConnectionString();

        public abstract IServiceProvider GetServiceProvider();

        /// <summary>
        /// 重写以获取是否开启延迟加载代理特性
        /// </summary>
        /// <returns></returns>
        public abstract bool LazyLoadingProxiesEnabled();
        /// <summary>
        /// 重写以实现数据上下文选项构建器加载数据库驱动程序
        /// </summary>
        /// <param name="builder">数据上下文选项构建器</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据上下文选项构建器</returns>
        public abstract DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString);
    }

}
