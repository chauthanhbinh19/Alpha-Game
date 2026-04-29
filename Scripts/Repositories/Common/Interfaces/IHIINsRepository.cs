using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIINsRepository
{
    Task<HIINs> GetHIINsAsync(string type);
    Task InsertOrUpdateHIINsAsync(string user_id, HIINs HIINs, string id);
    Task<HIINs> GetSumHIINsAsync(string user_id);
}