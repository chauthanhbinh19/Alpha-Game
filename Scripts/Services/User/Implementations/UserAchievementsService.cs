using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class UserAchievementsService : IUserAchievementsService
{
    private static UserAchievementsService _instance;
    private readonly IUserAchievementsRepository _userAchievementsRepository;

    public UserAchievementsService(IUserAchievementsRepository userAchievementsService)
    {
        _userAchievementsRepository = userAchievementsService;
    }

    public static UserAchievementsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAchievementsService(new UserAchievementsRepository());
        }
        return _instance;
    }

    public async Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _userAchievementsRepository.GetUserAchievementsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAchievementsCountAsync(string userId, string search, string rare)
    {
        return await _userAchievementsRepository.GetUserArchievementsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserAchievementAsync(Achievements achievement, string userId)
    {
        return await _userAchievementsRepository.InsertUserAchievementsAsync(achievement, userId);
    }

    public async Task<bool> UpdateUserAchievementLevelAsync(string userId, Achievements achievement)
    {
        return await _userAchievementsRepository.UpdateUserAchievementLevelAsync(userId, achievement);
    }

    public async Task<bool> UpdateUserAchievementStarAsync(string userId, Achievements achievement)
    {
        return await _userAchievementsRepository.UpdateUserAchievementStarAsync(userId, achievement);
    }

    public async Task<bool> UpdateUserAchievementBreakthroughAsync(string userId, Achievements achievement, int star, double quantity)
    {
        return await _userAchievementsRepository.UpdateUserAchievementBreakthroughAsync(userId, achievement, star, quantity);
    }

    public async Task<Achievements> GetUserAchievementByIdAsync(string userId, string Id)
    {
        return await _userAchievementsRepository.GetUserAchievementByIdAsync(userId, Id);
    }

    public async Task<Achievements> SumPowerUserAchievementsAsync(string userId)
    {
        return await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements)
    {
        return await _userAchievementsRepository.InsertOrUpdateUserAchievementsBatchAsync(userId, achievements);
    }
}