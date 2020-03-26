using Sprite.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModeCore.Order
{
    public class Order : EntityBase<int>
    {
        public string Code { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
