using System.Threading.Tasks;

public interface IMastersRepository
{
    Task<Masters> GetMasterByIdAsync(string id);
}