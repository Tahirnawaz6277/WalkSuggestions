using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class SQLWalkRepository:IwalkRepository
    {
        private readonly NZWalksDBContext dbcontext;

        public SQLWalkRepository(NZWalksDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Walk> CreatAsync(Walk walk)
        {
            await dbcontext.Walks.AddAsync(walk);
            await dbcontext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deletwalk =  dbcontext.Walks.FirstOrDefault(x => x.Id == id);
            if (deletwalk == null)
            {
                return null;
            }
             dbcontext.Walks.Remove(deletwalk);
            await dbcontext.SaveChangesAsync();
            return deletwalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            // Filtring
            var walks = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if (filterOn.Equals("Name" ,StringComparison.OrdinalIgnoreCase ))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

                //Pagination

                var skipResults = (pageNumber - 1) * (pageSize);
            

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
           return await dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
            var existingwalk = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk == null)
            {
                return null;
            }
            existingwalk.Name = walk.Name;
            existingwalk.Description = walk.Description;
            existingwalk.WalkImageUrl = walk.WalkImageUrl;
            existingwalk.LengthInKm = walk.LengthInKm;
            existingwalk.Region = walk.Region;
            existingwalk.Difficulty = walk.Difficulty;

            await dbcontext.SaveChangesAsync();
            return existingwalk;
        }
    }
}
