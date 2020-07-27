using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicInterfaces
{
    //Интерфейс логики для взаимодействия с данными регионов
    public interface IRegionLogic : IDisposable
    {
        Task<Region> GetRegionById(int regionId);

        Task<Region> GetRegionByName(string name);

        Task DeleteRegionFromDb(int regionId);

        Task UpdateRegion(Region editedRegion);

        Task AddNewRegion(Region region);
    }
}