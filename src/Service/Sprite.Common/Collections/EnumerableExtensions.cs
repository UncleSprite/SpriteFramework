using Sprite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprite.Common.Collections
{
    /// <summary>
    /// Enumerable集合扩展方法
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            source.CheckNotNull("source");
            source = source as IList<T> ?? source.ToList();
            return condition ? source.Where(predicate) : source;
        }
    }
}
