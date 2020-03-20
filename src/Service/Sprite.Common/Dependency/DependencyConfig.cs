using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Dependency
{
    /// <summary>
    /// 依赖注入配置项
    /// </summary>
    public static class DependencyConfig
    {
        /// <summary>
        /// 添加依赖注入初始化
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyConfig(this IServiceCollection services)
        {
            IDependencyTypeFinder dependencyTypeFinder = services.GetOrAddTypeFinder<IDependencyTypeFinder>(assemblyFinder => new DependencyTypeFinder(assemblyFinder));
            Type[] dependencyTypes = dependencyTypeFinder.FindAll();

            foreach (var denpendencyType in dependencyTypes)
            {
                // 注册服务
                AddToServices(services, denpendencyType);
            }

            return services;
        }

        private static void AddToServices(IServiceCollection services, Type implementationType)
        {
            if (implementationType.IsAbstract || implementationType.IsInterface)
                return;

            var lifeTime = GetLiftTimeOrNull(implementationType);
            if (lifeTime == null)
                return;

            DependencyAttribute dependencyAttribute = implementationType.GetAttribute<DependencyAttribute>();


        }

        /// <summary>
        /// 从类型获取要注册的生命周期类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ServiceLifetime? GetLiftTimeOrNull(Type type)
        {
            DependencyAttribute dependencyAttribute = type.GetAttribute<DependencyAttribute>();
            if (dependencyAttribute != null)
                return dependencyAttribute.Lifetime;

            if (type.IsDeriveClassFrom<ITransientDependency>())
                return ServiceLifetime.Transient;
            else if (type.IsDeriveClassFrom<IScopeDependency>())
                return ServiceLifetime.Scoped;
            else if (type.IsDeriveClassFrom<ISingletonDependency>())
                return ServiceLifetime.Singleton;

            return null;
        }

        /// <summary>
        /// 获取实现类型所有可注册服务接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type[] GetImplementedInterfaces(Type type)
        {
            return null;
        }
    }
}
