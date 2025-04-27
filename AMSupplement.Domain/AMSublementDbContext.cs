using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Supplemnt>()
                .ToTable("Suplements")
                .Property(x => x.Name).IsRequired();
        }
    }
}
