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
            SettingKey = "Sound.Music",
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - SFX setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Sound.SFX",
            SettingValue = "100",
            ValueType = "int" 
        });

        //Sound - Voice setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Sound.Voice",
            SettingValue = "100",
            ValueType = "int" 
        });

        //Graphic - Resolution setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Graphic.Resolution",
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Texture setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Graphic.Texture",
            SettingValue = "High",
            ValueType = "string" 
        });

        //Graphic - Damage Flytext setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Graphic.DamageFlytext",
            SettingValue = "On",
            ValueType = "string" 
        });

        //Graphic - In-game cinematic setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Graphic.InGameCinematic",
            SettingValue = "On",
            ValueType = "string" 
        });

        //Language - language setting
        InsertUserSettings(userId, new UserSettings
        {
            SettingKey = "Language",
            SettingValue = "vi",
            ValueType = "string" 
        });
    }
}
