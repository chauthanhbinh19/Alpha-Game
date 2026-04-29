using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArchitecturesService
{
    Task<List<Architectures>> GetUserArchitecturesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchitecturesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserArchitectureAsync(Architectures architecture, string userId);
    Task<bool> InsertOrUpdateUserArchitecturesBatchAsync(List<Architectures> architectures);
    Task<bool> UpdateArchitectureLevelAsync(Architectures architecture, int level);
    Task<bool> UpdateArchitectureBreakthroughAsync(Architectures architecture, int star, double quantity);
    Task<Architectures> GetUserArchitectureByIdAsync(string user_id, string Id);
    Task<Architectures> SumPowerUserArchitecturesAsync();
}