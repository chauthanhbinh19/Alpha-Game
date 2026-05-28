using System.Threading.Tasks;

public interface IArchivesService
{
    Task<Archives> GetArchiveByIdAsync(string id);
}