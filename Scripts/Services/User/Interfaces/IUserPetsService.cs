using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPetsService
{
    Task<List<Pets>> GetAllEquipmentPowerAsync(string user_id, List<Pets> PetsList);
    Task<List<Pets>> GetAllRankPowerAsync(string user_id, List<Pets> PetsList);
    Task<List<Pets>> GetAllMasterPowerAsync(string user_id, List<Pets> PetsList);
    Task<List<Pets>> GetUserPetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<Pets>> GetUserPetsTeamAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniquePetsTypesTeamAsync(string teamId);
    Task<int> GetUserPetsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserPetAsync(Pets pet, string userId);
    Task<bool> InsertOrUpdateUserPetsBatchAsync(List<Pets> pets);
    Task<bool> UpdatePetLevelAsync(Pets pet, int level);
    Task<bool> UpdatePetBreakthroughAsync(Pets pet, int star, double quantity);
    Task<bool> UpdateTeamPetAsync(string team_id, string card_id);
    Task<Pets> GetUserPetByIdAsync(string user_id, string Id);
    Task<List<Pets>> GetAllUserPetsInTeamAsync(string user_id);
}