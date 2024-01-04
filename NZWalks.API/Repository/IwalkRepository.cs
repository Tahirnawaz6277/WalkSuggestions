﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IwalkRepository
    {
        Task<Walk> CreatAsync(Walk walk);
        Task<Walk?> UpdateAsync( Guid id,Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null , string? filterQuery = null,
            string? sortBy = null , bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
