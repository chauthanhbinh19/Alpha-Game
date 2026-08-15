using System.Threading.Tasks;

public interface IHIDCsService
{
    Task<HIDCs> GetHIDCByIdAsync(string id);
    Task<InsertOrUpdateResult<HIDCs>> InsertHIDCAsync(HIDCs hidc);
    Task<InsertOrUpdateResult<HIDCs>> UpdateHIDCAsync(HIDCs hidc);
}