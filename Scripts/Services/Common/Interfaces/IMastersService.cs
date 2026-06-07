using System.Threading.Tasks;

public interface IMastersService
{
    Task<Masters> GetMasterByIdAsync(string id);
}