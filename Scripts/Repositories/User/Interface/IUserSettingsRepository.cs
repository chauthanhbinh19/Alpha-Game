using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSettingsRepository
{
    Task<List<UserSettings>> GetUserSettingsAsync(string userId);
    Task InsertUserSettingsAsync(string userId, UserSettings userSetting);
    Task UpdateUserSettingsAsync(string userId, UserSettings userSetting);
}