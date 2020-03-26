using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprite.EntityFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.EntityConfiguration.Order
{
    public class OrderConfiguration : EntityTypeConfigurationBase<ModeCore.Order.Order, int>
    {
        public override void Configure(EntityTypeBuilder<ModeCore.Order.Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(t => t.Price).HasColumnType("decimal(18, 2)");
            builder.Property(t => t.Code).HasMaxLength(32).IsRequired();

            builder.HasIndex(t => t.Code).IsUnique(true);
        }
    }
}
