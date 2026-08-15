using System.Threading.Tasks;

public interface IHICAsRepository
{
    Task<HICAs> GetHICAByIdAsync(string id);
    Task<InsertOrUpdateResult<HICAs>> InsertHICAAsync(HICAs hica);
    Task<InsertOrUpdateResult<HICAs>> UpdateHICAAsync(HICAs hica);
}