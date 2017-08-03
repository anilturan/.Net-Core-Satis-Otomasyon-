using Abc.Northwind.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //UseSqlServer EF Core İmplementasyonu.
            //EF Core şu an bilinen tüm DB'leri destekliyor.

            //UseSqlServer için Microsoft.EntityFrameworkCore.SqlServer kuruluyor.
            //UseSqlServer: SqlServer providerlarını destekler.
            optionsBuilder.UseSqlServer("Server=.;Database=NORTHWND2;Integrated Security=true;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
