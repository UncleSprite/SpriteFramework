using Sprite.Common.Entity.Base;

namespace Demo.ModeCore.Order
{
    public class OrderItem : EntityBase<int>
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Num { get; set; }
    }
}
