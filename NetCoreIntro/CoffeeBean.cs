namespace NetCoreIntro
{
    public class CoffeeBean
    {
        public CoffeeBean(long id, string country, string varietal)
        {
            Id = id;
            Country = country;
            Varietal = varietal;
        }

        public long Id { get; private set; }
        public string Country { get; private set; }
        public string Varietal { get; private set; }
    }
}