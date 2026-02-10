using AMSupplement.Domain.AuditEntityInterfaces;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AM_Supplement.Infrastructure.Persistence
{
    public class AMSublementDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AMSublementDbContext(DbContextOptions<AMSublementDbContext> options)
            : base(options)
        {
        }

        // --- الجداول (DbSets) ---
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. استدعاء إعدادات Identity الأساسية (ضروري جداً)
            base.OnModelCreating(modelBuilder);

            // 2. تطبيق جميع الـ Configurations الموجودة في المشروع تلقائياً
            // هذا السطر يبحث عن أي كلاس ينفذ IEntityTypeConfiguration ويطبقه
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AMSublementDbContext).Assembly);

            // 3. (اختياري) تخصيص أسماء جداول Identity إذا أردت تغييرها من AspNetUsers إلى Users مثلاً
            //modelBuilder.Entity<ApplicationUser>(entity => { entity.ToTable("Users"); });
            //modelBuilder.Entity<ApplicationRole>(entity => { entity.ToTable("Roles"); });
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        }

        // --- المعالجة التلقائية للتواريخ (Auditing) ---
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ApplyAuditing();
            return base.SaveChanges();
        }

        private void ApplyAuditing()
        {
            // البحث عن الكيانات التي ترث من AuditableEntity وتم تغييرها أو إضافتها
            var entries = ChangeTracker.Entries<AuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // الحفاظ على تاريخ الإنشاء الأصلي دون تغيير عند التعديل
                    entry.Property(x => x.CreatedDate).IsModified = false;
                }

                // تحديث تاريخ آخر تعديل في كل الحالات
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}