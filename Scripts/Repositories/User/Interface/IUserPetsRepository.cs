using System.Collections.Generic;

public interface IUserPetsRepository
{
    List<Pets> GetUserPets(string user_id, string type, int pageSize, int offset, string rare);
    List<Pets> GetUserPetsTeam(string user_id, string teamId);
    Dictionary<string, int> GetUniquePetTypesTeam(string teamId);
    int GetUserPetsCount(string user_id, string type, string rare);
    bool InsertUserPets(Pets pets, string userId);
    bool UpdatePetsLevel(Pets pets, int cardLevel);
    bool UpdatePetsBreakthrough(Pets pets, int star, double quantity);
    bool UpdateTeamCardPets(string team_id, string card_id);
    Pets GetUserPetsById(string user_id, string Id);
    List<Pets> GetAllUserPetsInTeam(string user_id);
}