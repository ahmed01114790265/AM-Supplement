using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
        {
           builder
                .ToTable(" OrderItems")
                .HasKey(x => x.Id);
           builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);
            builder
               .HasOne(x => x.Product)
               .WithMany(x => x.OrderItems)
               .HasForeignKey(x => x.ProductId);
            builder
                .Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
