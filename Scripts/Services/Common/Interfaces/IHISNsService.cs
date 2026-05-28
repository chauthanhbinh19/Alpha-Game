using System.Threading.Tasks;

public interface IHISNsService
{
    Task<HISNs> GetHISNByIdAsync(string id);
}