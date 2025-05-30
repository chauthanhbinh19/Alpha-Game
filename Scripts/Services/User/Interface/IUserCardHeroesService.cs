using System.Collections.Generic;

public interface IUserCardHeroesService
{
    List<CardHeroes> GetFinalPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllEquipmentPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllRankPower(string user_id, List<CardHeroes> CardHeroesList);
    List<CardHeroes> GetAllAnimeStatsPower(string user_id, List<CardHeroes> CardHeroesList);
    CardHeroes GetNewLevelPower(CardHeroes c, double coefficient);
    CardHeroes GetNewBreakthroughPower(CardHeroes c, double coefficient);
    List<CardHeroes> GetUserCardHeroes(string user_id, string type, int pageSize, int offset);
    List<CardHeroes> GetUserCardHeroesTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardHeroTypesTeam(string teamId);
    int GetUserCardHeroesCount(string user_id, string type);
    int GetUserCardHeroesTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardHeroes(CardHeroes CardHeroes);
    bool UpdateCardHeroesLevel(CardHeroes cardHeroes, int cardLevel);
    bool UpdateCardHeroesBreakthrough(CardHeroes cardHeroes, int star, int quantity);
    bool InsertFactCardHeroes(CardHeroes cardHeroes);
    bool UpdateFactCardHeroes(CardHeroes cardHeroes);
    bool UpdateTeamFactCardHeroes(string team_id, string position, string card_id);
    CardHeroes GetUserCardHeroesById(string user_id, string Id);
    List<CardHeroes> GetAllUserCardHeroesInTeam(string user_id);
}