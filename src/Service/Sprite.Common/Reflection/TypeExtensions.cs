using Sprite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="TAttribute">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null<</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
            return attributes.FirstOrDefault() as TAttribute;
        }

        /// <summary>
        /// 判断当前类型是否能由指定类型派生
        /// </summary>
        /// <typeparam name="TBaseType"></typeparam>
        /// <param name="type"></param>
        /// <param name="canAbstract"></param>
        /// <returns></returns>
        public static bool IsDeriveClassFrom<TBaseType>(this Type type, bool canAbstract = false)
        {
            return type.IsDeriveClassFrom(typeof(TBaseType), canAbstract);
        }

        /// <summary>
        /// 判断当前类型是否能由指定类型派生
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <param name="canAbstract"></param>
        /// <returns></returns>
        public static bool IsDeriveClassFrom(this Type type, Type baseType, bool canAbstract = false)
        {
            type.CheckNotNull("type");
            baseType.CheckNotNull("baseType");
            return type.IsClass && ((!canAbstract && !type.IsAbstract) || canAbstract) && type.IsBaseOn(baseType);
        }

        /// <summary>
        /// 返回当前类型是否能由指定基类派生
        /// </summary>
        /// <typeparam name="TBaseType"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBaseOn<TBaseType>(this Type type)
        {
            return IsBaseOn(type, typeof(TBaseType));
        }

        /// <summary>
        /// 当前类型是否是指定基类的派生类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition)
                return baseType.IsGenericTypeAssignableFrom(type);

            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 指定当前泛型是否可由指定的类型填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericTypeAssignableFrom(this Type genericType, Type type)
        {
            genericType.CheckNotNull("genericType");
            type.CheckNotNull("type");

            if (!genericType.IsGenericType)
                throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");

            List<Type> allOthers = new List<Type>();
            if (genericType.IsInterface)
                allOthers.AddRange(type.GetInterfaces());

            foreach (var other in allOthers)
            {
                Type cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                        cur = cur.GetGenericTypeDefinition();

                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                        return true;

                    cur = cur.BaseType;
                }
            }
            return false;
        }
    }
}
