using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {

           builder
                .ToTable("Oredrs")
                .HasKey(x => x.Id);
           builder
                .HasOne(x => x.User).WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);
           builder
                .Property(x => x.OrderDate).HasDefaultValueSql("GETDATE()")
                .IsRequired();
            builder
                .Property(x => x.TotalAmount).IsRequired();

        }
    }
}
