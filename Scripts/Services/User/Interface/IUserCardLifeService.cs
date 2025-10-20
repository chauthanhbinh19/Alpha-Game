using System.Collections.Generic;

public interface IUserCardLifeService
{
    CardLives GetNewLevelPower(CardLives c, double coefficient);
    CardLives GetNewBreakthroughPower(CardLives c, double coefficient);
    List<CardLives> GetUserCardLife(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserCardLifeCount(string user_id, string type, string rare);
    bool InsertUserCardLife(CardLives CardLife);
    bool UpdateCardLifeLevel(CardLives CardLife, int cardLevel);
    bool UpdateCardLifeBreakthrough(CardLives CardLife, int star, int quantity);
    CardLives GetUserCardLifeById(string user_id, string Id);
    CardLives SumPowerUserCardLife();
}