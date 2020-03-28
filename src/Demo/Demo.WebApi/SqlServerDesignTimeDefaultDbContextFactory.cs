using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Reflection;
using Sprite.EntityFrameWorkCore;
using Sprite.EntityFrameWorkCore.Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Demo.WebApi
{
    public class SqlServerDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public SqlServerDesignTimeDefaultDbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(configuration);

            services.AddSingleton<IAllAssemblyFinder, AppDomainAllAssemblyFinder>();
            services.AddSingleton<IEntityConfigurationTypeFinder, EntityConfigurationTypeFinder>();

            _serviceProvider = services.BuildServiceProvider();
        }

        public SqlServerDesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override string GetConnectionString()
        {
            var configuration = _serviceProvider.GetService<IConfigurationRoot>();
            return configuration.GetSection("ConnectionStrings:DefaultDatabase")?.Value;
        }

        public override IServiceProvider GetServiceProvider()
        {

            return _serviceProvider;
        }

        public override bool LazyLoadingProxiesEnabled()
        {

            return true;
        }

        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //return builder.UseSqlServer(connString, b => b.MigrationsAssembly(entryAssemblyName));
            return builder.UseSqlServer(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
    }
}
