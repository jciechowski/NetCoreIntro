using Microsoft.EntityFrameworkCore;

namespace NetCoreIntro
{
    public class CoffeeDBContext : DbContext
    {
        public CoffeeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoffeeBean> CoffeeBeans { get; set; }

        public override int SaveChanges()
        {
            LocalStore.Messages.Add("there was some change");

            return base.SaveChanges();
        }
    }
}