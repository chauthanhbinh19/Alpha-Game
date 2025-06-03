using System.Collections.Generic;

public interface ICardAdmiralsService
{
    List<string> GetUniqueCardAdmiralsTypes();
    List<CardAdmirals> GetCardAdmirals(string type, int pageSize, int offset);
    int GetCardAdmiralsCount(string type);    
    List<CardAdmirals> GetCardAdmiralsRandom(string type, int pageSize);
    List<CardAdmirals> GetAllCardAdmirals(string type);
    CardAdmirals GetCardAdmiralsById(string Id);
    List<CardAdmirals> GetCardAdmiralsWithPrice(string type, int pageSize, int offset);
    int GetCardAdmiralsWithPriceCount(string type);
}