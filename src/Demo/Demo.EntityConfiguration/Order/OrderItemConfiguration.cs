using Demo.ModeCore.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprite.EntityFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.EntityConfiguration.Order
{
    public class OrderItemConfiguration : EntityTypeConfigurationBase<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property("OrderItems");

            builder.Property(t => t.Name).HasMaxLength(32);
        }
    }
}
