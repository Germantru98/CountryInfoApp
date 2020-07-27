using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    //Интерфейс логики взаимодействия с информацией городов
    public interface ICityBL : IDisposable
    {
        Task<City> GetCityByName(string cityName);
    }
}