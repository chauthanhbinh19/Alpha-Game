using System.Threading.Tasks;

public interface IHIHNsRepository
{
    Task<HIHNs> GetHIHNByIdAsync(string id);
}