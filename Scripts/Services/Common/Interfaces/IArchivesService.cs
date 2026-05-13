using System.Collections.Generic;
using System.Threading.Tasks;
public interface IArchivesService
{ 
    Task<Archives> GetArchivesAsync(string id);
    Task InsertOrUpdateArchivesAsync(string user_id, Archives Archives, string id);
    Task<Archives> GetSumArchivesAsync(string user_id);
}