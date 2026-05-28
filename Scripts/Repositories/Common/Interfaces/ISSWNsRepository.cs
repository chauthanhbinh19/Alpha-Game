using System.Threading.Tasks;

public interface ISSWNsRepository
{
    Task<SSWNs> GetSSWNByIdAsync(string id);
}