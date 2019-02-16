using Microsoft.EntityFrameworkCore;
using TGNet.Models;

namespace TGNet
{
    public class CoffeeDBContext : DbContext
    {
        public CoffeeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoffeeBean> CoffeeBeans { get; set; }
    }
}