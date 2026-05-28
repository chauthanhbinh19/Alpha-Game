using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSettingsService
{ 
    Task<List<UserSettings>> GetUserSettingsAsync(string userId);
    Task InsertUserSettingAsync(string userId, UserSettings userSetting);
    Task UpdateUserSettingAsync(string userId, UserSettings userSetting);
    Task CreateInitiateUserSettingsAsync(string userId);
}