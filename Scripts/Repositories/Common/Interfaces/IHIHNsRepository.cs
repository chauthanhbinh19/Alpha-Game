using System.Threading.Tasks;

public interface IHIHNsRepository
{
    Task<HIHNs> GetHIHNByIdAsync(string id);
    Task<InsertOrUpdateResult<HIHNs>> InsertHIHNAsync(HIHNs hihn);
    Task<InsertOrUpdateResult<HIHNs>> UpdateHIHNAsync(HIHNs hihn);
}