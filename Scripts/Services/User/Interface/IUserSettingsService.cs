using System.Collections.Generic;

public interface IUserSettingsService
{ 
    List<UserSettings> GetUserSettings(string userId);
    void InsertUserSettings(string userId, UserSettings userSetting);
    void UpdateUserSettings(string userId, UserSettings userSetting);
    void CreateInitiateUserSettings(string userId);
}