using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Entity.Base
{
    /// <summary>
    /// 定义创建时间
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 获取或设置  创建时间
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}
