using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Dependency
{
    /// <summary>
    /// 依赖注入查找器，查找标注了<see cref="DependencyAttribute"/>特性
    /// 或者<see cref="ISingletonDependency"/>,<see cref="IScopeDependency"/>,<see cref="ITransientDependency"/>三个接口的服务实现类型
    /// </summary>
    public interface IDependencyTypeFinder : ITypeFinder
    {
    }
}
