using System.Threading.Tasks;

public interface IHIINsRepository
{
    Task<HIINs> GetHIINByIdAsync(string id);
}