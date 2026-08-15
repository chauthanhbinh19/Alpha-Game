using System.Threading.Tasks;

public interface IHIRNsService
{
    Task<HIRNs> GetHIRNByIdAsync(string id);
    Task<InsertOrUpdateResult<HIRNs>> InsertHIRNAsync(HIRNs hirn);
    Task<InsertOrUpdateResult<HIRNs>> UpdateHIRNAsync(HIRNs hirn);
}