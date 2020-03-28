using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var dependencyType in dependencyTypes)
            {
                // 注册服务
                AddToServices(services, dependencyType);
            }

            return services;
        }

        private static void AddToServices(IServiceCollection services, Type implementationType)
        {
            if (implementationType.IsAbstract || implementationType.IsInterface)
                return;

            ServiceLifetime? lifetime = GetLiftTimeOrNull(implementationType);
            if (lifetime == null)
            {
                return;
            }
            DependencyAttribute dependencyAttribute = implementationType.GetAttribute<DependencyAttribute>();
            Type[] serviceTypes = GetImplementedInterfaces(implementationType);

            // 服务数量位0  注册自身
            if (serviceTypes.Length == 0)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));
                return;
            }

            //服务实现显示要求注册身处时，注册自身并且继续注册接口
            if (dependencyAttribute?.AddSelf == true)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));
            }

            // 注册服务
            for (int i = 0; i < serviceTypes.Length; i++)
            {
                Type serviceType = serviceTypes[i];
                ServiceDescriptor descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime.Value);

                if (lifetime.Value == ServiceLifetime.Transient)
                {
                    services.TryAddEnumerable(descriptor);
                    continue;
                }

                bool multiple = serviceType.HasAttribute<MultipleDependencyAttribute>();
                if (i == 0)
                {
                    if (multiple)
                    {
                        services.Add(descriptor);
                    }
                    else
                    {
                        AddSingleService(services, descriptor, dependencyAttribute);
                    }
                }
                else
                {
                    //有多个接口，后边的接口注册使用第一个接口的实例，保证同个实现类的多个接口获得同一实例
                    Type firstServiceType = serviceTypes[0];
                    descriptor = new ServiceDescriptor(serviceType, provider => provider.GetService(firstServiceType), lifetime.Value);
                    if (multiple)
                    {
                        services.Add(descriptor);
                    }
                    else
                    {
                        AddSingleService(services, descriptor, dependencyAttribute);
                    }
                }
            }
        }

        /// <summary>
        /// 重写以实现 从类型获取要注册的<see cref="ServiceLifetime"/>生命周期类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>生命周期类型</returns>
        private static ServiceLifetime? GetLiftTimeOrNull(Type type)
        {
            DependencyAttribute dependencyAttribute = type.GetAttribute<DependencyAttribute>();
            if (dependencyAttribute != null)
                return dependencyAttribute.Lifetime;

            if (type.IsDeriveClassFrom<IScopeDependency>())
            {
                return ServiceLifetime.Scoped;
            }
            else if (type.IsDeriveClassFrom<ISingletonDependency>())
            {
                return ServiceLifetime.Singleton;
            }
            else if (type.IsDeriveClassFrom<ITransientDependency>())
            {
                return ServiceLifetime.Transient;
            }

            return null;
        }

        /// <summary>
        /// 从实现类型获取所有可注册服务接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type[] GetImplementedInterfaces(Type type)
        {
            Type[] exceptTypes = new Type[] { typeof(IDisposable) };
            Type[] interfaceTypes = type.GetInterfaces().Where(t => !exceptTypes.Contains(t) && !t.HasAttribute<IgnoreDependencyAttribute>()).ToArray();
            for (int index = 0; index < interfaceTypes.Length; index++)
            {
                Type interfaceType = interfaceTypes[index];
                if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition && interfaceType.FullName == null)
                {
                    interfaceTypes[index] = interfaceType.GetGenericTypeDefinition();
                }
            }
            return interfaceTypes;
        }

        private static void AddSingleService(IServiceCollection services,
           ServiceDescriptor descriptor,
           [CanBeNull] DependencyAttribute dependencyAttribute)
        {
            if (dependencyAttribute?.ReplaceExisting == true)
            {
                services.Replace(descriptor);
            }
            else if (dependencyAttribute?.TryAdd == true)
            {
                services.TryAdd(descriptor);
            }
            else
            {
                services.Add(descriptor);
            }
        }
    }
}