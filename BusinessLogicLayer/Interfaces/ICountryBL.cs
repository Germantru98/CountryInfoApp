using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    //Интерфейс логики для взаимодействия с информацией о странах
    public interface ICountryBL : IDisposable
    {
        Task<CountryInfoDTO> GetCountryInfo(string countryName);

        Task<List<CountryInfoDTO>> GetCountryInfosFromDb();

        Task<Country> GetCountryByNumericCode(int code);

        Task AddNewCountry(Country country);

        Task UpdateCountry(Country country, City capital, Region region, CountryInfoDTO countryInfo);
    }
}