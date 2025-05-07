using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMSupplement.Domain.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(x => x.Id);

            builder.ToTable(x => x
                   .HasCheckConstraint("CK_User_Email_Format", "[Email] LIKE '%@%.%' AND CHARINDEX(' ', [Email]) = 0"));


            builder
                .Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .HasMaxLength(20);

        }
    }
}
