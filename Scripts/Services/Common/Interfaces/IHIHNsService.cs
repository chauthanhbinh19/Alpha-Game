using System.Threading.Tasks;

public interface IHIHNsService
{
    Task<HIHNs> GetHIHNByIdAsync(string id);
    Task<InsertOrUpdateResult<HIHNs>> InsertHIHNAsync(HIHNs hihn);
    Task<InsertOrUpdateResult<HIHNs>> UpdateHIHNAsync(HIHNs hihn);
}