using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprite.Common.Dependency
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 获取或添加指定类型查找器
        /// </summary>
        /// <typeparam name="TTypeFinder"></typeparam>
        /// <param name="services"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static TTypeFinder GetOrAddTypeFinder<TTypeFinder>(this IServiceCollection services, Func<IAllAssemblyFinder, TTypeFinder> factory)
            where TTypeFinder : class
        {
            return services.GetOrAddSigletonInstance<TTypeFinder>(() =>
            {
                IAllAssemblyFinder allAssemblyFinder = services.GetOrAddSigletonInstance<IAllAssemblyFinder>(() => new AppDomainAllAssemblyFinder());
                return factory(allAssemblyFinder);
            });
        }

        /// <summary>
        /// 如果指定的服务不存在，创建实例并添加
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>
        /// <param name="services"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static TServiceType GetOrAddSigletonInstance<TServiceType>(this IServiceCollection services, Func<TServiceType> factory)
            where TServiceType : class
        {
            var serviceType = (TServiceType)services.FirstOrDefault(m => m.ServiceType == typeof(TServiceType) && m.Lifetime == ServiceLifetime.Singleton)?.ImplementationInstance;
            if (serviceType == null)
            {
                serviceType = factory();
                services.AddSingleton<TServiceType>(serviceType);
            }
            return serviceType;
        }
    }
}
