using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

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


            modelBuilder.Entity<Order>()
                .ToTable("Oredrs")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.User).WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Order>()
                .Property(x => x.OrderDate).HasDefaultValueSql("GETDATE()")
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(x=> x.TotalAmount).IsRequired();
         


            modelBuilder.Entity<OrderItem>()
                .ToTable("OrederItems")
                .HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order).WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<OrderItem>()
               .HasOne(x => x.Product).WithMany(x => x.OrderItems)
               .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<OrderItem>()
                .Property(x=> x.Quantity).IsRequired();

            modelBuilder.Entity<Product>()
                .ToTable("product")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                .Property(x => x.Name).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(x => x.Description).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(x => x.Taste).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(x => x.Image).HasColumnType("nvarchar(300)")
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .ToTable("Payment")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Order).WithMany(x => x.Payments)
                .HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<Payment>()
                .Property(x => x.Paydate).HasDefaultValueSql("GETDATE()")
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(x => x.Paymentstaus).IsRequired().HasDefaultValue(PaymentStatus.New);


        }
    }
}
