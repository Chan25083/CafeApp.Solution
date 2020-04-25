using CafeApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("CafeApp")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
