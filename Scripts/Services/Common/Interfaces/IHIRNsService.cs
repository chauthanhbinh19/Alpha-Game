using System.Threading.Tasks;

public interface IHIRNsService
{
    Task<HIRNs> GetHIRNByIdAsync(string id);
}