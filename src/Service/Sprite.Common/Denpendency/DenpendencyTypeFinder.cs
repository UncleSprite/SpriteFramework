using Sprite.Common.Finder;
using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Sprite.Common.Denpendency
{
    /// <summary>
    /// 依赖注入类型查找器
    /// </summary>
    public class DenpendencyTypeFinder : FinderBase<Type>, IDenpendencyTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        public DenpendencyTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现查找所有项
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type[] baseTypes = new[] { typeof(IScopeDependency), typeof(ITransientDependency), typeof(ISingletonDependency) };
            Type[] types = _allAssemblyFinder.FindAll(true).SelectMany(assembly => assembly.GetTypes())
                           //.Where(type=> type.IsClass && !type.IsAbstract && !type.IsInterface && !type.HasAttribute)
                           .ToArray();
            throw new NotImplementedException();
        }
    }
}
