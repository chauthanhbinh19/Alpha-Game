using System.Threading.Tasks;

public interface IResearchsService
{
    Task<Researchs> GetResearchByIdAsync(string id);
}