using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserSettingsService : IUserSettingsService
{
     private static UserSettingsService _instance;
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository)
    {
        _userSettingsRepository = userSettingsRepository;
    }

    public static UserSettingsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSettingsService(new UserSettingsRepository());
        }
        return _instance;
    }

    public async Task<List<UserSettings>> GetUserSettingsAsync(string userId)
    {
        return await _userSettingsRepository.GetUserSettingsAsync(userId);
    }

    public async Task InsertUserSettingAsync(string userId, UserSettings userSetting)
    {
        await _userSettingsRepository.InsertUserSettingAsync(userId, userSetting);
    }

    public async Task UpdateUserSettingAsync(string userId, UserSettings userSetting)
    {
        await _userSettingsRepository.UpdateUserSettingAsync(userId, userSetting);
    }

    public async Task CreateInitiateUserSettingsAsync(string userId)
    {
        //Sound - Music setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.MUSIC,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - SFX setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.SFX,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - Voice setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.VOICE,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Graphic - Resolution setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.RESOLUTION,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Texture setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.TEXTURE,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Damage Flytext setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.DAMAGE_FLYTEXT,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Graphic - In-game cinematic setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.IN_GAME_CINEMATIC,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Language - language setting
        await InsertUserSettingAsync(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.LANGUAGE,
            SettingValue = "en",
            ValueType = "string" 
        });
    }
}
