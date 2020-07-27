using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicInterfaces
{
    //Интерфейс логики для взаимодействия с данными стран
    public interface ICountryLogic : IDisposable
    {
        Task<CountryInfo> GetCountryInfoFromApi(string countryName);

        Task<List<Country>> GetCountriesFromDb();

        Task<Country> GetCountryById(int countryId);

        Task DeleteCountryFromDb(int countryId);

        Task UpdateCountry(Country editedCountry);

        Task AddNewCountry(Country country);

        Task<Country> GetCountryByNumericCode(int code);
    }
}