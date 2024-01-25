using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RepositoryDesignPatternDomainDrivenDesign.Models.Services
{
    public class OnlineShopDbContext : DbContext
    {
        #region [ctor]
        public OnlineShopDbContext()
    {

    }
    #endregion

    #region [OnlineShopDbContext(DbContextOptions options)]
 
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
        {
        }
        #endregion

        #region [OnModelCreating(ModelBuilder modelBuilder)]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region [Props]
    public DbSet<Person>? Person { get; set; }
    public DbSet<Product>? Product { get; set; }
    #endregion
}
}
