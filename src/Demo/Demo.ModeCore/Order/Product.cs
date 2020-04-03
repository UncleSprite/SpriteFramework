using Sprite.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModeCore.Order
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : EntityBase<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
