using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Denpendency
{
    /// <summary>
    /// 实现次接口的类将被自动注册为 <see cref="ServiceLifetime.Singletion"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface ISingletonDependency
    {
    }
}
