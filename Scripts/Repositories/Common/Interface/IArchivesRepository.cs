using System.Collections.Generic;
using System.Threading.Tasks;
public interface IArchivesRepository
{
    Task<Archives> GetArchivesAsync(string type);
    Task InsertOrUpdateArchivesAsync(string user_id, Archives Archives, string id);
    Task<Archives> GetSumArchivesAsync(string user_id);
}