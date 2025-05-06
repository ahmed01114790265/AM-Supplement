using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        void IEntityTypeConfiguration<Payment>.Configure(EntityTypeBuilder<Payment> builder)
        {
           builder
            .ToTable("Payment")
              .HasKey(x => x.Id);
            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.OrderId);
            builder
                .Property(x => x.Paydate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
            builder
                .Property(x => x.Paymentstaus)
                .IsRequired()
                .HasDefaultValue(PaymentStatus.New);
        }
    }
}
