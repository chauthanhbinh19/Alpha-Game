using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserAlchemiesService
{
    Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserAlchemyAsync(Alchemies alchemy, string userId);
    Task<bool> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemies);
    Task<bool> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy);
    Task<bool> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy);
    Task<bool> UpdateUserAlchemyBreakthroughAsync(string userId, Alchemies alchemy, int star, double quantity);
    Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id);
    Task<Alchemies> SumPowerUserAlchemiesAsync(string userId);
}
