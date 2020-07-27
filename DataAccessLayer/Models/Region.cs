namespace DataAccessLayer.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Region()
        {
        }

        public Region(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Регион :{Name} {Id}";
        }
    }
}