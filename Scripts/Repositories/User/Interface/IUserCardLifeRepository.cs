using System.Collections.Generic;

public interface IUserCardLifeRepository
{
    List<CardLife> GetUserCardLife(string user_id, string type, int pageSize, int offset);
    int GetUserCardLifeCount(string user_id, string type);
    bool InsertUserCardLife(CardLife CardLife);
    bool UpdateCardLifeLevel(CardLife CardLife, int cardLevel);
    bool UpdateCardLifeBreakthrough(CardLife CardLife, int star, int quantity);
    CardLife GetUserCardLifeById(string user_id, string Id);
    CardLife SumPowerUserCardLife();
}