using Microsoft.EntityFrameworkCore;
using NetCoreIntro.Models;

namespace NetCoreIntro
{
    public class CoffeeDBContext : DbContext
    {
        public CoffeeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoffeeBean> CoffeeBeans { get; set; }
    }
}