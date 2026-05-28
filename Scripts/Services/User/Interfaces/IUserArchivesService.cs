using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserArchivesService
{ 
    Task<UserArchives> GetUserArchivesAsync(string id);
    Task InsertOrUpdateUserArchivesAsync(string user_id, UserArchives Archives, string id);
    Task<UserArchives> GetSumUserArchivesAsync(string user_id);
}