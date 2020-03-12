using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprite.Common.Finder
{
    /// <summary>
    /// 查找器基类
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class FinderBase<TItem> : IFinder<TItem>
    {
        //protected

        /// <summary>
        /// 是否已查找
        /// </summary>
        private bool Found = false;

        public TItem[] Find(Func<TItem, bool> predicate, bool fromCache = false)
        {
            return FindAll(fromCache).Where(predicate).ToArray();
        }

        public TItem[] FindAll(bool fromCache = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重写实现查找所有项
        /// </summary>
        /// <returns></returns>
        protected abstract TItem[] FindAllItems();
    }
}
