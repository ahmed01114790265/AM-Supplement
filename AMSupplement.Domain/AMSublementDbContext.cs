using AMSupplement.Domain.Entities;
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
        public DbSet<Supplemnt> Supplemnts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           #region supplement
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Supplemnt>()
                .ToTable("Suplements")
                .Property(x => x.Name).IsRequired();
            #endregion
            #region product

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasMany(x => x.Items)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            # endregion
        }
    }
}
