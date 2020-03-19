﻿using Sprite.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Extensions
{

    /// <summary>
    /// 把对象类型转换为指定类型
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 把对象类型转换为指定类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }

            if (conversionType.IsNullableType())
            {
                conversionType = conversionType.GetUnNullableType();
            }

            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }

            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            if (conversionType == typeof(string))
            {
                return value.ToString();
            }
            return Convert.ChangeType(value, conversionType);
        }


        /// <summary>
        /// 把对象类型转化为指定类型
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转化的源对象</param>
        /// <returns>转化后的指定类型的对象，转化失败引发异常。</returns>
        public static T CastTo<T>(this object value)
        {
            if (value == null && default(T) == null)
            {
                return default(T);
            }

            if (value.GetType() == typeof(T))
            {
                return (T)value;
            }

            object result = CastTo(value, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转化的源对象</param>
        /// <param name="defaultValue">转化失败返回的默认值</param>
        /// <returns>转化后的指定类型对象，转化失败时返回指定的默认值</returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

    }
}
