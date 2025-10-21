using System.Collections.Generic;

public interface IUserCardHeroesRepository
{
    List<CardHeroes> GetUserCardHeroes(string user_id, string type, int pageSize, int offset, string rare);
    List<CardHeroes> GetUserCardHeroesTeam(string user_id, string teamId, string position);
    List<CardHeroes> GetUserCardHeroesTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardHeroTypesTeam(string teamId);
    int GetUserCardHeroesCount(string user_id, string type, string rare);
    int GetUserCardHeroesTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardHeroesTeamsCount(string user_id, string team_id);
    bool InsertUserCardHeroes(CardHeroes CardHeroes);
    bool UpdateCardHeroesLevel(CardHeroes cardHeroes, int cardLevel);
    bool UpdateCardHeroesBreakthrough(CardHeroes cardHeroes, int star, int quantity);
    bool UpdateTeamCardHeroes(string team_id, string position, string card_id);
    CardHeroes GetUserCardHeroesById(string user_id, string Id);
    List<CardHeroes> GetAllUserCardHeroesInTeam(string user_id);
}