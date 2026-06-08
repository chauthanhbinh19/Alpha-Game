using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserArchivesRepository
{
    Task<UserArchives> GetUserArchivesAsync(string type);
    Task InsertOrUpdateUserArchivesAsync(string userId, UserArchives Archives, string id);
    Task<UserArchives> GetSumUserArchivesAsync(string userId);
}