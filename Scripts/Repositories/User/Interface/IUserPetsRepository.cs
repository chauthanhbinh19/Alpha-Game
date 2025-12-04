using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPetsRepository
{
    Task<List<Pets>> GetUserPetsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<Pets>> GetUserPetsTeamAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniquePetsTypesTeamAsync(string teamId);
    Task<int> GetUserPetsCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserPetAsync(Pets pets, string userId);
    Task<bool> UpdatePetLevelAsync(Pets pets, int cardLevel);
    Task<bool> UpdatePetBreakthroughAsync(Pets pets, int star, double quantity);
    Task<bool> UpdateTeamPetAsync(string team_id, string card_id);
    Task<Pets> GetUserPetByIdAsync(string user_id, string Id);
    Task<List<Pets>> GetAllUserPetsInTeamAsync(string user_id);
}