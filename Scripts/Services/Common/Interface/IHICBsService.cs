using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHICBsService
{ 
    Task<HICBs> GetHICBsAsync(string id);
    Task InsertOrUpdateHICBsAsync(string user_id, HICBs HICBs, string id);
    Task<HICBs> GetSumHICBsAsync(string user_id);
    HICBs EnhanceHICBs(HICBs research, int level, int multiplier = 1);
}