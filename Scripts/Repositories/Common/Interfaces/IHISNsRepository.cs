using System.Threading.Tasks;

public interface IHISNsRepository
{
    Task<HISNs> GetHISNByIdAsync(string id);
}