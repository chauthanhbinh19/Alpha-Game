using System;
using System.Collections.Generic;
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

    public List<UserSettings> GetUserSettings(string userId)
    {
        return _userSettingsRepository.GetUserSettings(userId);
    }

    public void InsertUserSettings(string userId, UserSettings userSetting)
    {
        _userSettingsRepository.InsertUserSettings(userId, userSetting);
    }

    public void UpdateUserSettings(string userId, UserSettings userSetting)
    {
        _userSettingsRepository.UpdateUserSettings(userId, userSetting);
    }

    public void CreateInitiateUserSettings(string userId)
    {
        //Sound - Music setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.MUSIC,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - SFX setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.SFX,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - Voice setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.VOICE,
            SettingValue = "100",
            ValueType = "int" 
        });

        //Graphic - Resolution setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.RESOLUTION,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Texture setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.TEXTURE,
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Damage Flytext setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.DAMAGE_FLYTEXT,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Graphic - In-game cinematic setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.IN_GAME_CINEMATIC,
            SettingValue = "On",
            ValueType = "string" 
        });

        //Language - language setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = AppConstants.Setting.LANGUAGE,
            SettingValue = "vi",
            ValueType = "string" 
        });
    }
}
