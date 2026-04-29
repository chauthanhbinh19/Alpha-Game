using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHICAsService
{ 
    Task<HICAs> GetHICAsAsync(string id);
    Task InsertOrUpdateHICAsAsync(string user_id, HICAs HICAs, string id);
    Task<HICAs> GetSumHICAsAsync(string user_id);
    HICAs EnhanceHICAs(HICAs research, int level, int multiplier = 1);
}