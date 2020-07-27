using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    //Интерфейс логики для взаимодействия с информацией о регионах
    public interface IRegionBL : IDisposable
    {
        Task<Region> GetRegionByName(string regionName);
    }
}