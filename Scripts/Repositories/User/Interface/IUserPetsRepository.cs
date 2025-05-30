using System.Collections.Generic;

public interface IUserPetsRepository
{
    List<Pets> GetUserPets(string user_id, string type, int pageSize, int offset);
    List<Pets> GetUserPetsTeam(string user_id, string teamId);
    Dictionary<string, int> GetUniquePetTypesTeam(string teamId);
    int GetUserPetsCount(string user_id, string type);
    bool InsertUserPets(Pets pets);
    bool UpdatePetsLevel(Pets pets, int cardLevel);
    bool UpdatePetsBreakthrough(Pets pets, int star, int quantity);
    bool InsertFactPets(Pets pets);
    bool UpdateFactPets(Pets pets);
    bool UpdateTeamFactCardPets(string team_id, string card_id);
    Pets GetUserPetsById(string user_id, string Id);
    List<Pets> GetAllUserPetsInTeam(string user_id);
}