using System.Collections.Generic;

public interface IUserCardsService
{
    Cards GetNewLevelPower(Cards c, double coefficient);
    Cards GetNewBreakthroughPower(Cards c, double coefficient);
    List<Cards> GetUserCards(string user_id, int pageSize, int offset, string rare);
    int GetUserCardsCount(string user_id, string rare);
    bool InsertUserCards(Cards Cards);
    bool UpdateCardsLevel(Cards Cards, int cardLevel);
    bool UpdateCardsBreakthrough(Cards Cards, int star, double quantity);
    Cards GetUserCardsById(string user_id, string Id);
    Cards SumPowerUserCards();
}