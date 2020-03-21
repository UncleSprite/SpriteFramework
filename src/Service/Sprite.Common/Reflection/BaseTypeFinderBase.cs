using Sprite.Common.Finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sprite.Common.Reflection
{
    /// <summary>
    /// 指定类型的实现类型查找器基类
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    public abstract class BaseTypeFinderBase<TBaseType> : FinderBase<Type>, ITypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;
        /// <summary>
        /// 初始化一个<see cref="BaseTypeFinderBase{TBaseType}"/>类型的新实例
        /// </summary>
        /// <param name="allAssemblyFinder"></param>
        protected BaseTypeFinderBase(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }
        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom<TBaseType>()).Distinct().ToArray();
        }
    }
}
