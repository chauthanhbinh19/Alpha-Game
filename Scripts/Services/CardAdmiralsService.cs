using System.Collections.Generic;

public class CardAdmiralsService : ICardAdmiralsService
{
    private readonly ICardAdmiralsRepository _cardAdmiralsRepository;

    public CardAdmiralsService(ICardAdmiralsRepository cardAdmiralsRepository)
    {
        _cardAdmiralsRepository = cardAdmiralsRepository;
    }

    public static CardAdmiralsService Create()
    {
        return new CardAdmiralsService(new CardAdmiralsRepository());
    }

    public List<string> GetUniqueCardAdmiralsTypes()
    {
        return _cardAdmiralsRepository.GetUniqueCardAdmiralsTypes();
    }

    public List<CardAdmirals> GetCardAdmirals(string type, int pageSize, int offset)
    {
        List<CardAdmirals> list = _cardAdmiralsRepository.GetCardAdmirals(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardAdmiralsCount(string type)
    {
        return _cardAdmiralsRepository.GetCardAdmiralsCount(type);
    }

    public List<CardAdmirals> GetCardAdmiralsRandom(string type, int pageSize)
    {
        return _cardAdmiralsRepository.GetCardAdmiralsRandom(type, pageSize);
    }

    public List<CardAdmirals> GetAllCardAdmirals(string type)
    {
        return _cardAdmiralsRepository.GetAllCardAdmirals(type);
    }

    public CardAdmirals GetCardAdmiralsById(string Id)
    {
        return _cardAdmiralsRepository.GetCardAdmiralsById(Id);
    }

    public List<CardAdmirals> GetCardAdmiralsWithPrice(string type, int pageSize, int offset)
    {
        List<CardAdmirals> list = _cardAdmiralsRepository.GetCardAdmiralsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardAdmiralsWithPriceCount(string type)
    {
        return _cardAdmiralsRepository.GetCardAdmiralsWithPriceCount(type);
    }
}