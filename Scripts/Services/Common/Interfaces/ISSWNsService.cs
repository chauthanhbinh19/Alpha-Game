using System.Threading.Tasks;

public interface ISSWNsService
{
    Task<SSWNs> GetSSWNByIdAsync(string id);
    Task<InsertOrUpdateResult<SSWNs>> InsertSSWNAsync(SSWNs sswn);
    Task<InsertOrUpdateResult<SSWNs>> UpdateSSWNAsync(SSWNs sswn);
}