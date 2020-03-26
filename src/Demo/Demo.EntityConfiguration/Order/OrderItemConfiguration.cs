using Demo.ModeCore.Order;
using Microsoft.EntityFrameworkCore;
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
            builder.ToTable("OrderItems");

            builder.Property(t => t.Name).HasMaxLength(32);

            builder.Property(t => t.Price).HasColumnType("decimal(18, 2)");
        }
    }
}
