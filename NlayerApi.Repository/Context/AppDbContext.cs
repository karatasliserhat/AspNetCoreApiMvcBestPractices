using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NlayerApi.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature[] {

             new ProductFeature{ Id=1, ProductId=1, Color="Kırmızı",  Height=200, Width=100},
             new ProductFeature{ Id=2, ProductId=2, Color="Yeşil",  Height=100, Width=200}
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
