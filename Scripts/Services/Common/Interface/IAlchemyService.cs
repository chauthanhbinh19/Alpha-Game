using System.Collections.Generic;

public interface IAlchemyService
{
    List<string> GetUniqueAlchemyTypes();
    List<string> GetUniqueAlchemyId();
    List<Alchemy> GetAlchemy(string type, int pageSize, int offset, string rare);
    int GetAlchemyCount(string type, string rare);
    List<Alchemy> GetAlchemyWithPrice(string type, int pageSize, int offset);
    int GetAlchemyWithPriceCount(string type);
    Alchemy GetAlchemyById(string Id);
    Alchemy SumPowerAlchemyPercent();
}