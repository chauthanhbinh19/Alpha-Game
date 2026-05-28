using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserArchivesRepository
{
    Task<UserArchives> GetUserArchivesAsync(string type);
    Task InsertOrUpdateUserArchivesAsync(string user_id, UserArchives Archives, string id);
    Task<UserArchives> GetSumUserArchivesAsync(string user_id);
}