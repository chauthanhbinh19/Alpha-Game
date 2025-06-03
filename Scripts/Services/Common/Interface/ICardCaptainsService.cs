using System.Collections.Generic;

public interface ICardCaptainsService
{
    List<string> GetUniqueCardCaptainsTypes();
    List<CardCaptains> GetCardCaptains(string type, int pageSize, int offset);
    int GetCardCaptainsCount(string type);
    List<CardCaptains> GetCardCaptainsRandom(string type, int pageSize);
    List<CardCaptains> GetAllCardCaptains(string type);
    CardCaptains GetCardCaptainsById(string Id);
    List<CardCaptains> GetCardCaptainsWithPrice(string type, int pageSize, int offset);
    int GetCardCaptainsWithPriceCount(string type);
}