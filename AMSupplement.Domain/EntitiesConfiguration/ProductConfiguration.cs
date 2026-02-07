using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Weight)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(x => x.Taste)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(300)
                   .HasDefaultValue("default-product.png")
                   .IsRequired();

            builder.Property(x => x.DiscountPercentage)
                   .HasDefaultValue(0);

            builder.Property(x => x.Stock)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.Type)
                   .IsRequired();

            // Indexes (مهم جدًا للأداء)
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.IsActive);
        }
    }

}
