using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 将实体配置类注册到上下文
    /// </summary>
    public interface IEntityRegister
    {
        /// <summary>
        /// 将当前实体类映射对象注册到上下文模型构建器中
        /// </summary>
        /// <param name="builder"></param>
        void RegisterTo(ModelBuilder builder);
    }
}
