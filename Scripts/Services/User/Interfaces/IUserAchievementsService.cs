using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserAchievementsService
{
    Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAchievementsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementAsync(string userId, Achievements achievement);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements);
    Task<bool> UpdateUserAchievementLevelAsync(string userId, Achievements achievement);
    Task<bool> UpdateUserAchievementStarAsync(string userId, Achievements achievement);
    Task<Achievements> GetUserAchievementByIdAsync(string userId, string id);
    Task<Achievements> SumPowerUserAchievementsAsync(string userId);
}
