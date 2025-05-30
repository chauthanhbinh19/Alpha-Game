using System.Collections.Generic;

public interface IUserCardCaptainsRepository
{
    List<CardCaptains> GetUserCardCaptains(string user_id, string type, int pageSize, int offset);
    List<CardCaptains> GetUserCardCaptainsTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardCaptainTypesTeam(string teamId);
    bool UpdateTeamFactCardCaptains(string team_id, string position, string card_id);
    int GetUserCardCaptainsCount(string user_id, string type);
    int GetUserCardCaptainsTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardCaptains(CardCaptains CardCaptains);
    bool UpdateCardCaptainsLevel(CardCaptains cardCaptains, int cardLevel);
    bool UpdateCardCaptainsBreakthrough(CardCaptains cardCaptains, int star, int quantity);
    bool InsertFactCardCaptains(CardCaptains cardCaptains);
    bool UpdateFactCardCaptains(CardCaptains cardCaptains);
    CardCaptains GetUserCardCaptainsById(string user_id, string Id);
    List<CardCaptains> GetAllUserCardCaptainsInTeam(string user_id);
}