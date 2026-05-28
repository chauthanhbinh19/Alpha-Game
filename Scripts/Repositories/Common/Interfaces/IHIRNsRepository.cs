using System.Threading.Tasks;

public interface IHIRNsRepository
{
    Task<HIRNs> GetHIRNByIdAsync(string id);
}