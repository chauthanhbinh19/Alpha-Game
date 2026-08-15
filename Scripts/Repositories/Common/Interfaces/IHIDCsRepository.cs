using System.Threading.Tasks;

public interface IHIDCsRepository
{
    Task<HIDCs> GetHIDCByIdAsync(string id);
    Task<InsertOrUpdateResult<HIDCs>> InsertHIDCAsync(HIDCs hidc);
    Task<InsertOrUpdateResult<HIDCs>> UpdateHIDCAsync(HIDCs hidc);
}