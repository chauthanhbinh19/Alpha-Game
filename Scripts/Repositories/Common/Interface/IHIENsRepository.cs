using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIENsRepository
{
    Task<HIENs> GetHIENsAsync(string type);
    Task InsertOrUpdateHIENsAsync(string user_id, HIENs HIENs, string id);
    Task<HIENs> GetSumHIENsAsync(string user_id);
}