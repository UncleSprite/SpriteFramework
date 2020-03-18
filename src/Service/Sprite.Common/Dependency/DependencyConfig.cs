using Microsoft.Extensions.DependencyInjection;
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
            }

            return services;
        }
    }
}
