using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    //Интерфейс логики действий доступных пользователю
    public interface IUserBL : IDisposable
    {
        Task<CountryInfoDTO> GetCountryInfo(string countryName);

        Task<List<CountryInfoDTO>> GetCountryInfosFromDb();

        Task SaveCountyInfo(CountryInfoDTO countryInfo);
    }
}