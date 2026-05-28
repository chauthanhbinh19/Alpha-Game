using System.Threading.Tasks;

public interface IHIDCsRepository
{
    Task<HIDCs> GetHIDCByIdAsync(string id);
}