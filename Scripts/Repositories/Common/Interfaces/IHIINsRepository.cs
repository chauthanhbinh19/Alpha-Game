using System.Threading.Tasks;

public interface IHIINsRepository
{
    Task<HIINs> GetHIINByIdAsync(string id);
    Task<InsertOrUpdateResult<HIINs>> InsertHIINAsync(HIINs hiin);
    Task<InsertOrUpdateResult<HIINs>> UpdateHIINAsync(HIINs hiin);
}