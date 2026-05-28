using System.Threading.Tasks;

public interface ISSWNsService
{
    Task<SSWNs> GetSSWNByIdAsync(string id);
}