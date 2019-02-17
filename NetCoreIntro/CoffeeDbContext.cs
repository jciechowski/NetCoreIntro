using Microsoft.EntityFrameworkCore;

namespace NetCoreIntro
{
    public class CoffeeDBContext : DbContext
    {
        private readonly LocalStore _localStore;

        public CoffeeDBContext(DbContextOptions options, LocalStore localStore) : base(options)
        {
            _localStore = localStore;
        }

        public DbSet<CoffeeBean> CoffeeBeans { get; set; }

        public override int SaveChanges()
        {
            _localStore.Messages.Add("there was some change");

            return base.SaveChanges();
        }
    }
}