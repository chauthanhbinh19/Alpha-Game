using System.Threading.Tasks;

public interface IArchivesRepository
{
    Task<Archives> GetArchiveByIdAsync(string id);
}