using Sprite.Common.Dependency;
using Sprite.Common.Finder;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sprite.Common.Reflection
{
    /// <summary>
    /// 程序集查找器
    /// </summary>
     [IgnoreDependency]
    public interface IAssemblyFinder : IFinder<Assembly>
    {
    }
}
