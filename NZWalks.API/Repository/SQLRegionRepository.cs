using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{                                     //this is RepositoryService class or file

    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbContext;

        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreatAsync(Region region)
        {
            // Domain model to dbcontext
            dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var deleteregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteregion == null)
            {
                return null; // Return null when the region is not found
            }

            dbContext.Regions.Remove(deleteregion);
            await dbContext.SaveChangesAsync();
            return deleteregion;
        }


        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingregion = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (existingregion == null)
            {
                return null;
            }
            // updated the new record with existing record
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingregion;
        }
    }
}
