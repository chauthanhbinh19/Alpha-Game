using System.Threading.Tasks;

public interface IResearchsService
{
    Task<Researchs> GetResearchByIdAsync(string id);
    Task<InsertOrUpdateResult<Researchs>> InsertResearchAsync(Researchs research);
    Task<InsertOrUpdateResult<Researchs>> UpdateResearchAsync(Researchs research);
}