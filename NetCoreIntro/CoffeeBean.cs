namespace NetCoreIntro
{
    public class CoffeeBean
    {
        public CoffeeBean(long id, string country)
        {
            Id = id;
            Country = country;
        }

        public long Id { get; protected set; }
        public string Country { get; protected set; }
    }
}