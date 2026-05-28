using System.Threading.Tasks;

public interface IHITNsService
{
    Task<HITNs> GetHITNByIdAsync(string id);
}