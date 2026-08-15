using System.Threading.Tasks;

public interface IMastersService
{
    Task<Masters> GetMasterByIdAsync(string id);
    Task<InsertOrUpdateResult<Masters>> InsertMasterAsync(Masters master);
    Task<InsertOrUpdateResult<Masters>> UpdateMasterAsync(Masters master);
}