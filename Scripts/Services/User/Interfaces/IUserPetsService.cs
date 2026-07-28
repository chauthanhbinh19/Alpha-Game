using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPetsService
{
    Task<List<Pets>> GetAllEquipmentPowerAsync(string userId, List<Pets> petList);
    Task<List<Pets>> GetAllRankPowerAsync(string userId, List<Pets> petList);
    Task<List<Pets>> GetAllMasterPowerAsync(string userId, List<Pets> petList);
    Task<List<Pets>> GetUserPetsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Pets>> GetUserPetsTeamAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserPetsTypesTeamAsync(string userId, string teamId);
    Task<int> GetUserPetsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserPetAsync(Pets pet, string userId);
    Task<bool> InsertOrUpdateUserPetsBatchAsync(string userId, List<Pets> pets);
    Task<bool> UpdateUserPetLevelAsync(string userId, Pets pet);
    Task<bool> UpdateUserPetStarAsync(string userId, Pets pet);
    Task<bool> UpdateUserPetBreakthroughAsync(string userId, Pets pet, int star, double quantity);
    Task<bool> UpdateTeamUserPetAsync(string userId, string teamId, string cardId);
    Task<Pets> GetUserPetByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}