﻿namespace NetCoreIntro
{
    public class CoffeeBean
    {
        public CoffeeBean(long id, string country, string varietal, double humidity)
        {
            Id = id;
            Country = country;
            Varietal = varietal;
            Humidity = humidity;
        }

        public long Id { get; private set; }
        public string Country { get; private set; }
        public string Varietal { get; private set; }
        public double Humidity { get; private set; }
    }
}