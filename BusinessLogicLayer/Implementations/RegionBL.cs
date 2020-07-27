using BusinessLogicLayer.Interfaces;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class RegionBL : IRegionBL
    {
        private IRegionLogic _regionLogic;
        private bool disposedValue;

        public RegionBL(IRegionLogic regionLogic)
        {
            _regionLogic = regionLogic;
        }

        public async Task<Region> GetRegionByName(string regionName)
        {
            return await _regionLogic.GetRegionByName(regionName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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