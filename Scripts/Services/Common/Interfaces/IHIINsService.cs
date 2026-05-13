using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIINsService
{ 
    Task<HIINs> GetHIINsAsync(string id);
    Task InsertOrUpdateHIINsAsync(string user_id, HIINs HIINs, string id);
    Task<HIINs> GetSumHIINsAsync(string user_id);
}