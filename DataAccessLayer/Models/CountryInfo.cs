namespace DataAccessLayer.Models
{
    public class CountryInfo
    {
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
        public string CountryCapital { get; set; }
        public double CountryArea { get; set; }
        public int CountryPopulation { get; set; }
        public string Region { get; set; }

        public CountryInfo(string countryName, int countryCode, string countryCapital, double countryArea, int countryPopulation, string region)
        {
            CountryName = countryName;
            CountryCode = countryCode;
            CountryCapital = countryCapital;
            CountryArea = countryArea;
            CountryPopulation = countryPopulation;
            Region = region;
        }

        public CountryInfo()
        {
        }
    }
}