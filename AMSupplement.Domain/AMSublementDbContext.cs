using AMSupplement.Domain.Entities;
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
        public DbSet<User> Supplemnts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Paymentprocess> Paymentprocesss { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .Property(x =>x.Name).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x =>x.Email).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x=> x.Password).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.PhoneNumber).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Address).HasMaxLength(20);


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
                .HasForeignKey(x => x.OredeId);
            modelBuilder.Entity<OrderItem>()
               .HasOne(x => x.Product).WithMany(x => x.OrderItems)
               .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<OrderItem>()
                .Property(x => x.Pricepurchase).IsRequired();
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
                .HasForeignKey(x => x.OrdeId);
            modelBuilder.Entity<Payment>()
                .HasOne(x=> x.Paymentprocess).WithMany(x=> x.Payments)
                .HasForeignKey(x=> x.PaymentprocessId);
            modelBuilder.Entity<Payment>()
                .Property(x => x.Paydate).HasDefaultValueSql("GETDATE()")
                .IsRequired();
            modelBuilder.Entity<Payment>()
                .Property(x=> x.Amountorders).IsRequired();
            modelBuilder.Entity<Payment>()
                .Property(x => x.Paymentstaus).IsRequired();

            modelBuilder.Entity<Paymentprocess>()
                .ToTable("PaymentProcess")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Paymentprocess>()
                .Property(x => x.ProcessName).IsRequired();
        }
    }
}
