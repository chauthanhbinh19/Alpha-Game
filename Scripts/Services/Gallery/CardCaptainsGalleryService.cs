using System.Collections.Generic;

public class CardCaptainsGalleryService : ICardCaptainsGalleryService
{
    private ICardCaptainsGalleryRepository _cardCaptainsGalleryRepository;

    public CardCaptainsGalleryService(ICardCaptainsGalleryRepository cardCaptainsGalleryRepository)
    {
        _cardCaptainsGalleryRepository = cardCaptainsGalleryRepository;
    }

    public static CardCaptainsGalleryService Create()
    {
        return new CardCaptainsGalleryService(new CardCaptainsGalleryRepository());
    }

    public List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset)
    {
        List<CardCaptains> list = _cardCaptainsGalleryRepository.GetCardCaptainsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardCaptainsCount(string type)
    {
        return _cardCaptainsGalleryRepository.GetCardCaptainsCount(type);
    }

    public void InsertCardCaptainsGallery(string Id)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        _cardCaptainsGalleryRepository.InsertCardCaptainsGallery(Id, _service.GetCardCaptainsById(Id));
    }

    public void UpdateStatusCardCaptainsGallery(string Id)
    {
        _cardCaptainsGalleryRepository.UpdateStatusCardCaptainsGallery(Id);
    }

    public CardCaptains SumPowerCardCaptainsGallery()
    {
        return _cardCaptainsGalleryRepository.SumPowerCardCaptainsGallery();
    }
}
