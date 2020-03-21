using Sprite.Common.Dependency;
using Sprite.Common.Finder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Reflection
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    [IgnoreDependency]
    public interface ITypeFinder : IFinder<Type>
    {
    }
}
