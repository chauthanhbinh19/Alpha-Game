using System.Collections.Generic;

public interface IAlchemyRepository
{
    List<string> GetUniqueAlchemyTypes();
    List<Alchemy> GetAlchemy(string type, int pageSize, int offset);
    int GetAlchemyCount(string type);
    List<Alchemy> GetAlchemyWithPrice(string type, int pageSize, int offset);
    int GetAlchemyWithPriceCount(string type);
    Alchemy GetAlchemyById(string Id);
    Alchemy SumPowerAlchemyPercent();
}