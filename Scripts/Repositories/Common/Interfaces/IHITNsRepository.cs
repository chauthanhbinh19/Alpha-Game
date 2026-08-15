using System.Threading.Tasks;

public interface IHITNsRepository
{
    Task<HITNs> GetHITNByIdAsync(string id);
    Task<InsertOrUpdateResult<HITNs>> InsertHITNAsync(HITNs hitn);
    Task<InsertOrUpdateResult<HITNs>> UpdateHITNAsync(HITNs hitn);
}