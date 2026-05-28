using System.Threading.Tasks;

public interface IHIENsService
{
    Task<HIENs> GetHIENByIdAsync(string id);
}