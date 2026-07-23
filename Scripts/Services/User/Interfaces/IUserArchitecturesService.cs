using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArchitecturesService
{
    Task<List<Architectures>> GetUserArchitecturesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchitecturesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserArchitectureAsync(Architectures architecture, string userId);
    Task<bool> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architectures);
    Task<bool> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture);
    Task<bool> UpdateUserArchitectureStarAsync(string userId, Architectures architecture);
    Task<bool> UpdateUserArchitectureBreakthroughAsync(string userId, Architectures architecture, int star, double quantity);
    Task<Architectures> GetUserArchitectureByIdAsync(string user_id, string Id);
    Task<Architectures> SumPowerUserArchitecturesAsync(string userId);
}