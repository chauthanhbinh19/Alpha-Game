using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArchitecturesRepository
{
    Task<List<Architectures>> GetUserArchitecturesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchitecturesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Architectures>> InsertOrUpdateUserArchitectureAsync(string userId, Architectures architecture);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Architectures>>> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architectures);
    Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureStarAsync(string userId, Architectures architecture);
    Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureQuantityAsync(string userId, Architectures architecture, List<RecipeItemDetailDTO> totalDeductions);
    Task<Architectures> GetUserArchitectureByIdAsync(string userId, string Id);
    Task<Architectures> SumPowerUserArchitecturesAsync(string userId);
}