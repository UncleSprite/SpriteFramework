using Sprite.Common.Dependency;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.EntityFrameWorkCore
{
    /// <summary>
    /// 实体类配置类查找器
    /// </summary>
    public class EntityConfigurationTypeFinder : BaseTypeFinderBase<IEntityRegister>, IEntityConfigurationTypeFinder, ISingletonDependency
    {
        public EntityConfigurationTypeFinder(IAllAssemblyFinder allAssemblyFinder) : base(allAssemblyFinder)
        {
        }
    }
}
