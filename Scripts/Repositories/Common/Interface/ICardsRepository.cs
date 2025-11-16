using System.Collections.Generic;

public interface ICardsRepository
{
    List<string> GetUniqueCardId();
    List<Cards> GetCards(int pageSize, int offset, string rare);
    int GetCardsCount(string rare);
    List<Cards> GetCardsWithPrice(int pageSize, int offset);
    int GetCardsWithPriceCount();
    Cards GetCardsById(string Id);
    Cards SumPowerCardsPercent();
}
