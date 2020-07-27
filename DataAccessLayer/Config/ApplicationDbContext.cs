using DataAccessLayer.Models;
using System.Data.Entity;

namespace DataAccessLayer.Config
{
    //Контекст базы данных EF6
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}