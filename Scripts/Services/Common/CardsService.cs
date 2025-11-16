using System.Collections.Generic;

public class CardsService : ICardsService
{
    private readonly ICardsRepository _CardsRepository;

    public CardsService(ICardsRepository titleRepository)
    {
        _CardsRepository = titleRepository;
    }

    public static CardsService Create()
    {
        return new CardsService(new CardsRepository());
    }

    public List<Cards> GetCards(int pageSize, int offset, string rare)
    {
        List<Cards> list = _CardsRepository.GetCards(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardsCount(string rare)
    {
        return _CardsRepository.GetCardsCount(rare);
    }

    public List<Cards> GetCardsWithPrice(int pageSize, int offset)
    {
        List<Cards> list = _CardsRepository.GetCardsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardsWithPriceCount()
    {
        return _CardsRepository.GetCardsWithPriceCount();
    }

    public Cards GetCardsById(string Id)
    {
        return _CardsRepository.GetCardsById(Id);
    }

    public Cards SumPowerCardsPercent()
    {
        return _CardsRepository.SumPowerCardsPercent();
    }

    public List<string> GetUniqueCardId()
    {
        return _CardsRepository.GetUniqueCardId();
    }
}
