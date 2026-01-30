using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArchitecturesRepository
{
    Task<List<Architectures>> GetUserArchitecturesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchitecturesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserArchitectureAsync(Architectures architectures, string userId);
    Task<bool> UpdateArchitectureLevelAsync(Architectures architectures, int cardLevel);
    Task<bool> UpdateArchitectureBreakthroughAsync(Architectures architectures, int star, double quantity);
    Task<Architectures> GetUserArchitectureByIdAsync(string user_id, string Id);
    Task<Architectures> SumPowerUserArchitecturesAsync();
}