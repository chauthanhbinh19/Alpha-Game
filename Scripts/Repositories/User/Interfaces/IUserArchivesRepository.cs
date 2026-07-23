using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserArchivesRepository
{
    Task<UserArchives> GetUserArchivesAsync(string userId, string id);
    Task InsertOrUpdateUserArchivesAsync(string userId, UserArchives Archives, string id);
    Task<UserArchives> GetSumUserArchivesAsync(string userId);
}