using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class UserBL : IUserBL
    {
        private ICountryBL _countryLogic;
        private ICityBL _cityLogic;
        private IRegionBL _regionLogic;
        private bool disposedValue;

        public UserBL(ICountryBL countryLogic, ICityBL cityLogic, IRegionBL regionLogic)
        {
            _countryLogic = countryLogic;
            _cityLogic = cityLogic;
            _regionLogic = regionLogic;
        }

        /// <summary>
        /// Возвращает объект, хранящий информацию о стране с указанным названием
        /// </summary>
        /// <param name="countryName">Название страны</param>
        /// <returns></returns>
        public async Task<CountryInfoDTO> GetCountryInfo(string countryName)
        {
            return await _countryLogic.GetCountryInfo(countryName);
        }
        /// <summary>
        /// Возвращает информацию обо всех странах, сохраненных в базе данных
        /// </summary>
        /// <returns></returns>
        public async Task<List<CountryInfoDTO>> GetCountryInfosFromDb()
        {
            return await _countryLogic.GetCountryInfosFromDb();
        }
        /// <summary>
        /// Добавляет указанную информацию о стране в базу данных
        /// </summary>
        /// <param name="countryInfo">Информация о стране</param>
        /// <returns></returns>
        public async Task SaveCountyInfo(CountryInfoDTO countryInfo)
        {
            var capital = await _cityLogic.GetCityByName(countryInfo.CountryCapital);
            if (capital == null)
            {
                capital = new City(countryInfo.CountryCapital);
            }
            var region = await _regionLogic.GetRegionByName(countryInfo.Region);
            if (region == null)
            {
                region = new Region(countryInfo.Region);
            }
            var country = await _countryLogic.GetCountryByNumericCode(countryInfo.CountryCode);
            if (country == null)
            {
                await _countryLogic.AddNewCountry(new Country()
                {
                    Name = countryInfo.CountryName,
                    Capital = capital,
                    Region = region,
                    Area = countryInfo.CountryArea,
                    CountryCode = countryInfo.CountryCode,
                    Population = countryInfo.CountryPopulation
                });
            }
            else
            {
                await _countryLogic.UpdateCountry(country, capital, region, countryInfo);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _regionLogic.Dispose();
                    _countryLogic.Dispose();
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