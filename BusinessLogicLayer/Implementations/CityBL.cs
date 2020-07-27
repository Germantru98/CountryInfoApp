using BusinessLogicLayer.Interfaces;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class CityBL : ICityBL
    {
        private ICityLogic _cityLogic;

        private bool disposedValue = false;

        public CityBL(ICityLogic cityLogic)
        {
            _cityLogic = cityLogic;
        }

        public async Task<City> GetCityByName(string cityName)
        {
            return await _cityLogic.GetCityByName(cityName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cityLogic.Dispose();
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