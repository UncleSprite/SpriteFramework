using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Denpendency
{
    /// <summary>
    /// 实现此接口的类将被注册为 <see cref="ServiceLifetime.Scope"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface IScopeDependency
    {
    }
}
