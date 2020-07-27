using DataAccessLayer.Config;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicImplementations
{
    //Реализация логики для получения данных о странах
    public class CountryLogic : ICountryLogic
    {
        private ApplicationDbContext _db;
        private bool disposedValue;

        public CountryLogic(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewCountry(Country country)
        {
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCountryFromDb(int countryId)
        {
            var country = await _db.Countries.FindAsync(countryId);
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Данный метод возвращает список всех городов, хранящихся в базе данных
        /// </summary>
        /// <returns></returns>
        public async Task<List<Country>> GetCountriesFromDb()
        {
            return await _db.Countries.Include(c => c.Capital).Include(c => c.Region).ToListAsync();
        }
        /// <summary>
        /// Возвращает объект, хранящий информацию о стране, соответствующей указанному идентификатору 
        /// </summary>
        /// <param name="countryId">Идентификатор страны</param>
        /// <returns></returns>
        public async Task<Country> GetCountryById(int countryId)
        {
            return await _db.Countries.Include(c => c.Capital).Include(c => c.Region).FirstOrDefaultAsync(c => c.Id == countryId);
        }
        /// <summary>
        /// Возвращает объект, хранящий информацию о стране, соответствующей указанному цифровому коду
        /// </summary>
        /// <param name="code">Код страны</param>
        /// <returns></returns>
        public async Task<Country> GetCountryByNumericCode(int code)
        {
            return await _db.Countries.Include(c => c.Capital).Include(c => c.Region).FirstOrDefaultAsync(c => c.CountryCode == code);
        }
        /// <summary>
        /// Производит запрос с указанным названием страны на API restcountries.eu и, в случае успеха операции,
        /// возвращает объект, содержащий информацию о стране
        /// </summary>
        /// <param name="countryName">Название страны</param>
        public async Task<CountryInfo> GetCountryInfoFromApi(string countryName)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string request = $"https://restcountries.eu/rest/v2/name/{countryName}";
                HttpResponseMessage response = (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JToken json = JToken.Parse(responseBody).First;
                //Парсинг JSON объекта по необходимым полям
                var country = new CountryInfo()
                {
                    CountryName = (string)json["name"],
                    CountryArea = GetDoubleFromJsonField((string)json["area"]),
                    CountryCapital = (string)json["capital"],
                    CountryCode = GetIntFromJsonField((string)json["numericCode"]),
                    CountryPopulation = GetIntFromJsonField((string)json["population"]),
                    Region = (string)json["region"]
                };

                return country;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
        /// <summary>
        /// Получает вещественное число из поля JSON
        /// </summary>
        /// <param name="data">Значение поля</param>
        /// <returns></returns>
        private double GetDoubleFromJsonField(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException();
            }
            double result;
            return double.TryParse(data, out result) ? result : 0.0;
        }
        /// <summary>
        /// Получает целое число из поля JSON
        /// </summary>
        /// <param name="data">Значение поля</param>
        /// <returns></returns>
        private int GetIntFromJsonField(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException();
            }
            int result;
            return int.TryParse(data, out result) ? result : 0;
        }

        public async Task UpdateCountry(Country editedCountry)
        {
            _db.Entry(editedCountry).State = EntityState.Modified;
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
            GC.SuppressFinalize(this);
        }
    }
}