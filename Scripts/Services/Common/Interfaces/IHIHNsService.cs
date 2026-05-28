using System.Threading.Tasks;

public interface IHIHNsService
{
    Task<HIHNs> GetHIHNByIdAsync(string id);
}