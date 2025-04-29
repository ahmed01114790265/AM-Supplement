project dependencies 
 # AM-Supplement.Presentation depends on ==>  AM-Supplement.Application , AM-Supplement.Contracts
 # AM-Supplement.Application depends on ==> AM-Supplement.Contracts, AM-Supplement.DataAccess, AM-Supplement.Shared
 # AM-Supplement.DataAccess depends on ==> AM-Supplement.Domain
 # AM-Supplement.Domain depends on ==> AM-Supplement.Shared 

 steps for adding EntityFramework
 # install the following packages :-
  ## Microsoft.EntityFrameworkCore
  ## Microsoft.EntityFrameworkCore.SqlServer
  ## Microsoft.EntityFrameworkCore.Tools
# create ProjectDbContext class and make it inherit from DbContext class 
# add DbSets for your model , ex: DbSet<Product> Products
# in ProjectDbContext class ovveride onConfiguring method and using passed options configure the dbContext using DbContextOptionsBuilder
# object , 
  ## base.OnConfiguring(optionsBuilder);
  ##  ptionsBuilder.UseSqlServer(Configuration.GetConnectionString("cs"));
# or you configure it during register dbContext in IOC container in program.cs

# in ProjectDbContext class ovveride onModelCreating method and within it configure entities 