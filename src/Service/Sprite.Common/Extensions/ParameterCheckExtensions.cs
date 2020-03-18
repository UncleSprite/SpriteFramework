using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Extensions
{
    public static class ParameterCheckExtensions
    {
        /// <summary>
        /// 验证指定值的断言<see cref="assertion"/>是否为真，如果不为真，抛出指定消息<paramref name="message"/>的指定类型<paramref name="TException"/>
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言</param>
        /// <param name="message">异常消息</param>
        private static void Require<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion)
            {
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }

        /// <summary>
        /// 检查参数不能为空引用，否则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNull<T>(this T value, string paramName) where T : class
        {
            Require<ArgumentNullException>(value != null, paramName);
        }
    }
}
