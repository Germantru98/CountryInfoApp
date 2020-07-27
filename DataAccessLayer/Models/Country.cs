namespace DataAccessLayer.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public City Capital { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
        public Region Region { get; set; }
    }
}