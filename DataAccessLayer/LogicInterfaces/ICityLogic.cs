using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicInterfaces
{
    //Интерфейс логики для взаимодействия с данными городов
    public interface ICityLogic : IDisposable
    {
        Task<City> GetCityById(int cityId);

        Task DeleteCityFromDb(int cityId);

        Task UpdateCity(City editedCity);

        Task AddNewCity(City city);

        Task<City> GetCityByName(string cityName);
    }
}