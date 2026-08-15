using System.Threading.Tasks;

public interface IScienceFictionsRepository
{
    Task<ScienceFictions> GetScienceFictionByIdAsync(string id);
    Task<InsertOrUpdateResult<ScienceFictions>> InsertScienceFictionAsync(ScienceFictions scienceFiction);
    Task<InsertOrUpdateResult<ScienceFictions>> UpdateScienceFictionAsync(ScienceFictions scienceFiction);
}