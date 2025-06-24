using AMSupplement.Domain.AuditEntityInterfaces;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.EntitiesConfiguration;
using AMSupplement.Domain.EntitiesConfiguration._ٍAuditableEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AMSupplement.Domain
{
    public class AMSublementDbContext : DbContext
    {
        IConfiguration Configuration;
        public AMSublementDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;  
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("cs"));
        //}
        #region dbsets
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> payments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder .ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());  
            modelBuilder.ApplyConfiguration(new ProductConfiguration());  
            modelBuilder .ApplyConfiguration(new PaymentConfiguration());

            modelBuilder.ApplyAuditProperties();
        }

        public override int SaveChanges()
        {
            ApplyAuditing();
            return base.SaveChanges(acceptAllChangesOnSuccess : true);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return await base.SaveChangesAsync(true, cancellationToken);
        }
        private void ApplyAuditing()
        {
            var currentDate = DateTime.UtcNow;
           // var currentUser = Guid.NewGuid(); // will be handled in another task 
            foreach(var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = currentDate;
                   // entry.Entity.CreatedBy = currentUser;
                }
                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = currentDate;
                    //entry.Entity.UpdatedBy = currentUser;

                    // optional: prevent tampering with CreatedDate/By
                    entry.Property(e => e.CreatedDate).IsModified = false;
                    entry.Property(e => e.CreatedBy).IsModified = false;
                }
            }
        }
    }
}
