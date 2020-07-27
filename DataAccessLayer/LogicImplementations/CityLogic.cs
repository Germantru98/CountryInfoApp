using DataAccessLayer.Config;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicImplementations
{
    //Реализация логики для получения данных о городах
    public class CityLogic : ICityLogic
    {
        private ApplicationDbContext _db;
        private bool disposedValue;

        public CityLogic(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewCity(City city)
        {
            _db.Cities.Add(city);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCityFromDb(int cityId)
        {
            var city = await _db.Cities.FindAsync(cityId);
            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
        }

        public async Task<City> GetCityByName(string cityName)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => c.Name == cityName);
        }

        public async Task<City> GetCityById(int cityId)
        {
            return await _db.Cities.FindAsync(cityId);
        }

        public async Task UpdateCity(City editedCity)
        {
            _db.Entry(editedCity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}