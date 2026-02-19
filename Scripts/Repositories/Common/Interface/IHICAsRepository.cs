using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHICAsRepository
{
    Task<HICAs> GetHICAsAsync(string type);
    Task InsertOrUpdateHICAsAsync(string user_id, HICAs HICAs, string id);
    Task<HICAs> GetSumHICAsAsync(string user_id);
}