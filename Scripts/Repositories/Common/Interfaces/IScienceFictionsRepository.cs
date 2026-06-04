using System.Threading.Tasks;

public interface IScienceFictionsRepository
{
    Task<ScienceFictions> GetScienceFictionByIdAsync(string id);
}