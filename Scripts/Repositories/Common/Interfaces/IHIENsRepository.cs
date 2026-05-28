using System.Threading.Tasks;

public interface IHIENsRepository
{
    Task<HIENs> GetHIENByIdAsync(string id);
}