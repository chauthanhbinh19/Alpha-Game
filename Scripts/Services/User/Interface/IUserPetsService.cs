using System.Collections.Generic;

public interface IUserPetsService
{
    List<Pets> GetFinalPower(string user_id, List<Pets> PetsList);
    List<Pets> GetAllEquipmentPower(string user_id, List<Pets> PetsList);
    List<Pets> GetAllRankPower(string user_id, List<Pets> PetsList);
    List<Pets> GetAllAnimeStatsPower(string user_id, List<Pets> PetsList);
    Pets GetNewLevelPower(Pets c, double coefficient);
    Pets GetNewBreakthroughPower(Pets c, double coefficient);
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