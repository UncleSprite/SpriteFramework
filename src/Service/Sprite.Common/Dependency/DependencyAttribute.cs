using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Dependency
{
    /// <summary>
    /// 依赖注入行为特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
    }
}
