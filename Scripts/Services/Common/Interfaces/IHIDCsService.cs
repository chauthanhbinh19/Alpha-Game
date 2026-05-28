using System.Threading.Tasks;

public interface IHIDCsService
{
    Task<HIDCs> GetHIDCByIdAsync(string id);
}