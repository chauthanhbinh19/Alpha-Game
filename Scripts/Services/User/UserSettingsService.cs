using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository)
    {
        _userSettingsRepository = userSettingsRepository;
    }

    public static UserSettingsService Create()
    {
        return new UserSettingsService(new UserSettingsRepository());
    }

    public async Task<List<UserSettings>> GetUserSettingsAsync(string userId)
    {
        return await _userSettingsRepository.GetUserSettingsAsync(userId);
    }

    public async Task InsertUserSettingsAsync(string userId, UserSettings userSetting)
    {
        await _userSettingsRepository.InsertUserSettingsAsync(userId, userSetting);
    }

    public async Task UpdateUserSettingsAsync(string userId, UserSettings userSetting)
    {
        await _userSettingsRepository.UpdateUserSettingsAsync(userId, userSetting);
    }

    public async Task CreateInitiateUserSettingsAsync(string userId)
    {
        //Sound - Music setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.MUSIC,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - SFX setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.SFX,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - Voice setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.VOICE,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Graphic - Resolution setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.RESOLUTION,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Texture setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.TEXTURE,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Damage Flytext setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.DAMAGE_FLYTEXT,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Graphic - In-game cinematic setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.IN_GAME_CINEMATIC,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Language - language setting
        await InsertUserSettingsAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.LANGUAGE,
            SettingValue = "en",
            ValueType = "string" 
        });
    }
}
