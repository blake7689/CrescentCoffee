using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrescentCoffee.Models
{
    public class CrescentCoffeeDbContext : IdentityDbContext
    {
        public CrescentCoffeeDbContext(DbContextOptions<CrescentCoffeeDbContext>
            options) : base(options) 
        {
        }

        public DbSet<CoffeeType> CoffeeTypes { get; set; }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
