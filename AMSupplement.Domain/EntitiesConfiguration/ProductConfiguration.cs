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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                 .ToTable("products")
                 .HasKey(x => x.Id);
           builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(x => x.Price)
                .IsRequired();
            builder
                .Property(x => x.Taste)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(x => x.ImageUrl)
                .HasColumnType("nvarchar(300)")
                .IsRequired();
        }
    }
}
