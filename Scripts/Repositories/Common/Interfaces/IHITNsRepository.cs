using System.Threading.Tasks;

public interface IHITNsRepository
{
    Task<HITNs> GetHITNByIdAsync(string id);
}