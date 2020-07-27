using DataAccessLayer.Config;
using DataAccessLayer.LogicInterfaces;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccessLayer.LogicImplementations
{
    public class RegionLogic : IRegionLogic
    {
        private ApplicationDbContext _db;
        private bool disposedValue;

        public RegionLogic(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewRegion(Region region)
        {
            _db.Regions.Add(region);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRegionFromDb(int regionId)
        {
            var region = await _db.Regions.FindAsync(regionId);
            _db.Regions.Remove(region);
            await _db.SaveChangesAsync();
        }

        public async Task<Region> GetRegionById(int regionId)
        {
            return await _db.Regions.FindAsync(regionId);
        }

        public async Task<Region> GetRegionByName(string name)
        {
            return await _db.Regions.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task UpdateRegion(Region editedRegion)
        {
            _db.Entry(editedRegion).State = EntityState.Modified;
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
            System.GC.SuppressFinalize(this);
        }
    }
}