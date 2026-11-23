using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardGeneralsService
{
    Task<List<CardGenerals>> GetAllEquipmentPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetAllRankPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetAllMasterPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetUserCardGeneralsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamAsync(string userId, string teamId, string position);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamWithoutPositionAsync(string userId, string teamId);
    Task<List<CardGenerals>> GetUserCardGeneralsInTeamAsync(string userId);
    Task<List<CardGenerals>> GetUserCardGeneralsInTeamSimpleAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardGeneralsTypesTeamAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> UpdateTeamUserCardGeneralAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardGeneralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardGeneralsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardGeneralsTeamsCountAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardGeneralAsync(string userId, CardGenerals cardGeneral);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardGeneralsBatchAsync(string userId, List<CardGenerals> cardGenerals);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralLevelAsync(string userId, CardGenerals cardGeneral, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralStarAsync(string userId, CardGenerals cardGeneral);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralQuantityAsync(string userId, CardGenerals cardGeneral, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralAffinityAsync(string userId, CardGenerals cardGeneral, List<RecipeItemDetailDTO> totalDeductions);
    Task<CardGenerals> GetUserCardGeneralByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
}