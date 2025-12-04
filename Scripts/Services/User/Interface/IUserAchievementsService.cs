using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserAchievementsService
{
    Task<Achievements> GetNewLevelPowerAsync(Achievements c, double coefficient);
    Task<Achievements> GetNewBreakthroughPowerAsync(Achievements c, double coefficient);
    Task<List<Achievements>> GetUserAchievementsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserAchievementsCountAsync(string user_id, string rare);
    Task<bool> InsertUserAchievementAsync(Achievements achievements, string userId);
    Task<bool> UpdateAchievementLevelAsync(Achievements achievements, int cardLevel);
    Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievements, int star, double quantity);
    Task<Achievements> GetUserAchievementByIdAsync(string user_id, string id);
    Task<Achievements> SumPowerUserAchievementsAsync();
}
