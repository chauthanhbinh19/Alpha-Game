using System.Threading.Tasks;

public interface IResearchsRepository
{
    Task<Researchs> GetResearchByIdAsync(string id);
}