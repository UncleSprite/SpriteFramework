using System;
using System.Collections.Generic;
using System.Text;

namespace Sprite.Common.Finder
{
    public interface IFinder<TItem>
    {
        /// <summary>
        /// 查找指定条件的项目
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="fromCache"></param>
        /// <returns></returns>
        TItem[] Find(Func<TItem, bool> predicate, bool fromCache = false);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        TItem[] FindAll(bool fromCache = false);
    }
}
