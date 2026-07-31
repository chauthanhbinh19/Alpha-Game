using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserAlchemiesService
{
    Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemyAsync(string userId, Alchemies alchemy);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemies);
    Task<bool> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy);
    Task<bool> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy);
    Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id);
    Task<Alchemies> SumPowerUserAlchemiesAsync(string userId);
}
