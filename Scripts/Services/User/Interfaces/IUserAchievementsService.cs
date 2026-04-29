using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserAchievementsService
{
    Task<List<Achievements>> GetUserAchievementsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAchievementsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserAchievementAsync(Achievements achievement, string userId);
    Task<bool> InsertOrUpdateUserAchievementsBatchAsync(List<Achievements> achievements);
    Task<bool> UpdateAchievementLevelAsync(Achievements achievement, int level);
    Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievement, int star, double quantity);
    Task<Achievements> GetUserAchievementByIdAsync(string user_id, string id);
    Task<Achievements> SumPowerUserAchievementsAsync();
}
