using System.Threading.Tasks;

public interface IHIINsService
{
    Task<HIINs> GetHIINByIdAsync(string id);
}