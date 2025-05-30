using System.Collections.Generic;

public class CardLifeGalleryService : ICardLifeGalleryService
{
    private readonly ICardLifeGalleryRepository _cardLifeGalleryRepository;

    public CardLifeGalleryService(ICardLifeGalleryRepository cardLifeGalleryRepository)
    {
        _cardLifeGalleryRepository = cardLifeGalleryRepository;
    }

    public static CardLifeGalleryService Create()
    {
        return new CardLifeGalleryService(new CardLifeGalleryRepository());
    }

    public List<CardLife> GetCardLifeCollection(string type, int pageSize, int offset)
    {
        List<CardLife> list = _cardLifeGalleryRepository.GetCardLifeCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardLifeCount(string type)
    {
        return _cardLifeGalleryRepository.GetCardLifeCount(type);
    }

    public void InsertCardLifeGallery(string Id)
    {
        ICardLifeRepository _repository = new CardLifeRepository();
        CardLifeService _service = new CardLifeService(_repository);
        _cardLifeGalleryRepository.InsertCardLifeGallery(Id, _service.GetCardLifeById(Id));
    }

    public void UpdateStatusCardLifeGallery(string Id)
    {
        _cardLifeGalleryRepository.UpdateStatusCardLifeGallery(Id);
    }

    public CardLife SumPowerCardLifeGallery()
    {
        return _cardLifeGalleryRepository.SumPowerCardLifeGallery();
    }
}
