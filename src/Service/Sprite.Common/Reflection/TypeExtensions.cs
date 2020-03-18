using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sprite.Common.Reflection
{
    /// <summary>
    /// 类型<see cref="Type"/>扩展方法
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="methodInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true)
            where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
        }
    }
}
