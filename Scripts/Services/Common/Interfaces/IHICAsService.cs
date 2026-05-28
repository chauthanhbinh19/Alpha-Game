using System.Threading.Tasks;

public interface IHICAsService
{
    Task<HICAs> GetHICAByIdAsync(string id);
}