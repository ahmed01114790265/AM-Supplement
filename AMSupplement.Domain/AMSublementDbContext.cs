using AMSupplement.Domain.Entities;
using AMSupplement.Domain.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AMSupplement.Domain
{
    public class AMSublementDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        IConfiguration Configuration;
        public AMSublementDbContext(DbContextOptions<AMSublementDbContext> options, IConfiguration configuration) : base(options)
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
        }
    }
}
