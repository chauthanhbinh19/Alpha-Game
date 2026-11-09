using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBeveragesRepository
{
    Task<List<Beverages>> GetUserBeveragesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBeveragesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Beverages>> InsertOrUpdateUserBeverageAsync(string userId, Beverages beverage);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Beverages>>> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beverages);
    Task<InsertOrUpdateResult<bool>> UpdateUserBeverageLevelAsync(string userId, Beverages beverage, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserBeverageStarAsync(string userId, Beverages beverage);
    Task<InsertOrUpdateResult<bool>> UpdateUserBeverageQuantityAsync(string userId, Beverages beverage, List<RecipeItemDetailDTO> totalDeductions);
    Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id);
    Task<Beverages> SumPowerUserBeveragesAsync(string userId);
}