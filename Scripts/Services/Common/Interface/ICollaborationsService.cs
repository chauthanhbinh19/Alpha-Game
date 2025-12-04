using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsService
{
    Task<List<string>> GetUniqueCollaborationsIdAsync();
    Task<List<Collaborations>> GetCollaborationsAsync(int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string rare);
    Task<List<Collaborations>> GetCollaborationsWithPriceAsync(int pageSize, int offset);
    Task<int> GetCollaborationsWithPriceCountAsync();
    Task<Collaborations> GetCollaborationByIdAsync(string id);
    Task<Collaborations> SumPowerCollaborationsPercentAsync();
}
