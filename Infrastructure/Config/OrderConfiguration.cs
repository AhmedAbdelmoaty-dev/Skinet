﻿using Domain.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.ShippingAddress, o => o.WithOwner());
            builder.OwnsOne(x => x.PaymentSummary, o => o.WithOwner());
            builder.Property(x => x.Status).HasConversion(
                o=>o.ToString(),
                o=> (OrderStatus)Enum.Parse(typeof(OrderStatus),o)
            );
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.OrderDate).HasConversion(
                x => x.ToUniversalTime(),
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc) //makes sure that .net knows its utc when retrived from database
            );
        }
    }
}
