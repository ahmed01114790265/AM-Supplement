using AMSupplement.Domain.AuditEntityInterfaces;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.EntitiesConfiguration._ٍAuditableEntities
{
    public static class AuditableEntityConfiguration
    {
        public static void ApplyAuditProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.CreatedDate))
                        .IsRequired()
                        .HasColumnType("datetime2");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.CreatedBy))
                        .IsRequired(false)
                        .HasColumnType("uniqueidentifier");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.UpdatedDate))
                        .IsRequired(false)
                        .HasColumnType("datetime2");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.UpdatedBy))
                        .IsRequired(false)
                        .HasColumnType("uniqueidentifier");
                }
            }
        }
    }

}
