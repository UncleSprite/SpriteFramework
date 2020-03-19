using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Entity.Base
{
    /// <summary>
    /// 数据接口模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
