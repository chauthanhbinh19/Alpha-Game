using System.Threading.Tasks;

public interface IHICAsRepository
{
    Task<HICAs> GetHICAByIdAsync(string id);
}