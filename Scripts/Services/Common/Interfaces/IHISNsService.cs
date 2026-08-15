using System.Threading.Tasks;

public interface IHISNsService
{
    Task<HISNs> GetHISNByIdAsync(string id);
    Task<InsertOrUpdateResult<HISNs>> InsertHISNAsync(HISNs hisn);
    Task<InsertOrUpdateResult<HISNs>> UpdateHISNAsync(HISNs hisn);
}