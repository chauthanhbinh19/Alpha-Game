using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsService
{
    Task<List<string>> GetUniqueCollaborationsIdAsync();
    Task<List<Collaborations>> GetCollaborationsAsync(string search, string rare, int pageSize, int offset);
    Task<List<Collaborations>> GetCollaborationsWithoutLimitAsync();
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task<List<Collaborations>> GetCollaborationsWithPriceAsync(int pageSize, int offset);
    Task<int> GetCollaborationsWithPriceCountAsync();
    Task<Collaborations> GetCollaborationByIdAsync(string id);
    Task<Collaborations> SumPowerCollaborationsPercentAsync();
}
