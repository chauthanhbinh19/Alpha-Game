using System.Collections.Generic;

public interface IAlchemyRepository
{
    List<string> GetUniqueAlchemyTypes();
    List<string> GetUniqueAlchemyId();
    List<Alchemies> GetAlchemy(string type, int pageSize, int offset, string rare);
    int GetAlchemyCount(string type, string rare);
    List<Alchemies> GetAlchemyWithPrice(string type, int pageSize, int offset);
    int GetAlchemyWithPriceCount(string type);
    Alchemies GetAlchemyById(string Id);
    Alchemies SumPowerAlchemyPercent();
}