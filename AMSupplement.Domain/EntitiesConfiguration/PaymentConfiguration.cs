using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PayDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(x => x.PaymentStatus)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasDefaultValue(PaymentStatus.New);

            builder.Property(x => x.PaymentMethod)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.Payments)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.OrderId);
            builder.HasIndex(x => x.PaymentStatus);
        }
    }

}
