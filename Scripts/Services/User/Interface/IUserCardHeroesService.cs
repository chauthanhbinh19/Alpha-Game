using System.Collections.Generic;

public interface IUserCardHeroesService
{
    List<CardHeroes> GetFinalPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllEquipmentPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllRankPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllAnimeStatsPower(string user_id, List<CardHeroes> CardHeroesList);
    CardHeroes GetNewLevelPower(CardHeroes c, double coefficient);
    CardHeroes GetNewBreakthroughPower(CardHeroes c, double coefficient);
    List<CardHeroes> GetSkills(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetUserCardHeroes(string user_id, string type, int pageSize, int offset, string rare);
    List<CardHeroes> GetUserCardHeroesTeam(string user_id, string teamId, string position);
    List<CardHeroes> GetUserCardHeroesTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardHeroTypesTeam(string teamId);
    int GetUserCardHeroesCount(string user_id, string type, string rare);
    int GetUserCardHeroesTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardHeroesTeamsCount(string user_id, string team_id);
    bool InsertUserCardHeroes(CardHeroes CardHeroes);
    bool UpdateCardHeroesLevel(CardHeroes cardHeroes, int cardLevel);
    bool UpdateCardHeroesBreakthrough(CardHeroes cardHeroes, int star, double quantity);
    bool UpdateTeamCardHeroes(string team_id, string position, string card_id);
    CardHeroes GetUserCardHeroesById(string user_id, string Id);
    List<CardHeroes> GetAllUserCardHeroesInTeam(string user_id);
}