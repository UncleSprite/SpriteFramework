using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Entity.Base
{
    /// <summary>
    /// 数据库表必有字段
    /// </summary>
    public interface IDataBase : ICreatedTime, ISoftDeletable
    {
        Guid? CreatedById { get; set; }
        Guid? UpdatedById { get; set; }
        DateTime? UpdatedAt { get; set; }
        //bool Deleted { get; set; }
        Guid? DeletedById { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
