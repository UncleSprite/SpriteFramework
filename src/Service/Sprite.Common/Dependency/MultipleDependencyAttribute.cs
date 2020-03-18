using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Dependency
{
    /// <summary>
    /// 标记允许多重继承，即一个接口可以注册多个实例
    /// </summary>
    public class MultipleDependencyAttribute : Attribute
    {
    }
}
