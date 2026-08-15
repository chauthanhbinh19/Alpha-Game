using System.Threading.Tasks;

public interface IHICAsService
{
    Task<HICAs> GetHICAByIdAsync(string id);
    Task<InsertOrUpdateResult<HICAs>> InsertHICAAsync(HICAs hica);
    Task<InsertOrUpdateResult<HICAs>> UpdateHICAAsync(HICAs hica);
}