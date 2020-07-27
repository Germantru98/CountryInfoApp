namespace BusinessLogicLayer.Models
{
    public class CountryInfoDTO
    {
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
        public string CountryCapital { get; set; }
        public double CountryArea { get; set; }
        public int CountryPopulation { get; set; }
        public string Region { get; set; }

        public CountryInfoDTO()
        {
        }

        public CountryInfoDTO(string countryName, int countryCode, string countryCapital, double countryArea, int countryPopulation, string region)
        {
            CountryName = countryName;
            CountryCode = countryCode;
            CountryCapital = countryCapital;
            CountryArea = countryArea;
            CountryPopulation = countryPopulation;
            Region = region;
        }

        public override string ToString()
        {
            return $"Название страны: {CountryName}\n Регион: {Region}\n Столица: {CountryCapital}\n Население страны: {CountryPopulation}\n" +
                $"Площадь страны: {CountryArea}";
        }
    }
}