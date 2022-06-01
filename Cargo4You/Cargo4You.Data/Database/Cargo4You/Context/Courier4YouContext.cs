using Cargo4You.Data.Database.Cargo4You.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Data.Database.Cargo4You.Context
{
    public class Courier4YouContext : DbContext
    {
        public Courier4YouContext(DbContextOptions<Courier4YouContext> options) : base(options)
        {

        }


        public DbSet<Courier> Couriers { get; set; }
        public DbSet<CourierPrice> CourierPrices { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
