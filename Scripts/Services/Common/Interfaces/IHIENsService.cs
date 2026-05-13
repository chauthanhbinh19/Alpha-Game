using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIENsService
{ 
    Task<HIENs> GetHIENsAsync(string id);
    Task InsertOrUpdateHIENsAsync(string user_id, HIENs HIENs, string id);
    Task<HIENs> GetSumHIENsAsync(string user_id);
}