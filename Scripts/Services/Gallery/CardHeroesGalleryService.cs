using System.Collections.Generic;

public class CardHeroesGalleryService : ICardHeroesGalleryService
{
    private readonly ICardHeroesGalleryRepository _cardHeroesGalleryRepository;

    public CardHeroesGalleryService(ICardHeroesGalleryRepository cardHeroesGalleryRepository)
    {
        _cardHeroesGalleryRepository = cardHeroesGalleryRepository;
    }

    public static CardHeroesGalleryService Create()
    {
        return new CardHeroesGalleryService(new CardHeroesGalleryRepository());
    }

    public List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset)
    {
        List<CardHeroes> list = _cardHeroesGalleryRepository.GetCardHeroesCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardHeroesCount(string type)
    {
        return _cardHeroesGalleryRepository.GetCardHeroesCount(type);
    }

    public void InsertCardHeroesGallery(string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        _cardHeroesGalleryRepository.InsertCardHeroesGallery(Id, _service.GetCardHeroesById(Id));
    }

    public void UpdateStatusCardHeroesGallery(string Id)
    {
        _cardHeroesGalleryRepository.UpdateStatusCardHeroesGallery(Id);
    }

    public CardHeroes SumPowerCardHeroesGallery()
    {
        return _cardHeroesGalleryRepository.SumPowerCardHeroesGallery();
    }
}
