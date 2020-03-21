using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Entity.Base
{
    /// <summary>
    /// 定义逻辑删除功能
    /// </summary>
    public interface ISoftDeletable
    {
        bool Deleted { get; set; }
    }
}
