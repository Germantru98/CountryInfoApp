using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class CountryBL : ICountryBL
    {
        private ICountryLogic _countryLogic;
        private ICityLogic _cityLogic;
        private IRegionLogic _regionLogic;
        private bool disposedValue;

        public CountryBL(ICountryLogic countryLogic, ICityLogic cityLogic, IRegionLogic regionLogic)
        {
            _countryLogic = countryLogic;
            _cityLogic = cityLogic;
            _regionLogic = regionLogic;
        }

        public async Task AddNewCountry(Country country)
        {
            await _countryLogic.AddNewCountry(country);
        }

        public async Task<Country> GetCountryByNumericCode(int code)
        {
            return await _countryLogic.GetCountryByNumericCode(code);
        }

        public async Task<CountryInfoDTO> GetCountryInfo(string countryName)
        {
            var country = await _countryLogic.GetCountryInfoFromApi(countryName);
            return new CountryInfoDTO(country.CountryName, country.CountryCode, country.CountryCapital, country.CountryArea,
                country.CountryPopulation, country.Region);
        }

        public async Task<List<CountryInfoDTO>> GetCountryInfosFromDb()
        {
            var countriesFromDb = await _countryLogic.GetCountriesFromDb();
            var listOfCountriesInfoDTO = new List<CountryInfoDTO>();
            foreach (var country in countriesFromDb)
            {
                listOfCountriesInfoDTO.Add(new CountryInfoDTO(country.Name, country.CountryCode, country.Capital.Name, country.Area,
                country.Population, country.Region.Name));
            }
            return listOfCountriesInfoDTO;
        }

        public async Task UpdateCountry(Country country, City capital, Region region, CountryInfoDTO countryInfo)
        {
            country.Area = countryInfo.CountryArea;
            country.CountryCode = countryInfo.CountryCode;
            country.Name = countryInfo.CountryName;
            country.Population = countryInfo.CountryPopulation;
            await _countryLogic.UpdateCountry(country);
            await _cityLogic.UpdateCity(capital);
            await _regionLogic.UpdateRegion(region);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cityLogic.Dispose();
                    _countryLogic.Dispose();
                    _regionLogic.Dispose();
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