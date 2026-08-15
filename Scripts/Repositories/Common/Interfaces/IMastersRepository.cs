using System.Threading.Tasks;

public interface IMastersRepository
{
    Task<Masters> GetMasterByIdAsync(string id);
    Task<InsertOrUpdateResult<Masters>> InsertMasterAsync(Masters master);
    Task<InsertOrUpdateResult<Masters>> UpdateMasterAsync(Masters master);
}