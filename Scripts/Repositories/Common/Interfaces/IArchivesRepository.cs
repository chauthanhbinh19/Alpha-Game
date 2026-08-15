using System.Threading.Tasks;

public interface IArchivesRepository
{
    Task<Archives> GetArchiveByIdAsync(string id);
    Task<InsertOrUpdateResult<Archives>> InsertArchiveAsync(Archives archive);
    Task<InsertOrUpdateResult<Archives>> UpdateArchiveAsync(Archives archive);
}