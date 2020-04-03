using Demo.ModeCore.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprite.EntityFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.EntityConfiguration.Order
{
    public class ProductConfiguration : EntityTypeConfigurationBase<ModeCore.Order.Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                Name = "测试产品",
                Quantity = 1000
            });

            builder.Property(t => t.Price).HasColumnType("decimal(18, 2)");
        }
    }
}
