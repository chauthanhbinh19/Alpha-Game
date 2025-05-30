using System.Collections.Generic;

public class CardAdmiralsGalleryService : ICardAdmiralsGalleryService
{
    private ICardAdmiralsGalleryRepository _cardAdmiralsGalleryRepository;

    public CardAdmiralsGalleryService(ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository)
    {
        _cardAdmiralsGalleryRepository = cardAdmiralsGalleryRepository;
    }

    public static CardAdmiralsGalleryService Create()
    {
        return new CardAdmiralsGalleryService(new CardAdmiralsGalleryRepository());
    }

    public List<CardAdmirals> GetCardAdmiralsCollection(string type, int pageSize, int offset)
    {
        List<CardAdmirals> list = _cardAdmiralsGalleryRepository.GetCardAdmiralsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardAdmiralsCount(string type)
    {
        return _cardAdmiralsGalleryRepository.GetCardAdmiralsCount(type);
    }

    public void InsertCardAdmiralsGallery(string Id)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        _cardAdmiralsGalleryRepository.InsertCardAdmiralsGallery(Id, _service.GetCardAdmiralsById(Id));
    }

    public void UpdateStatusCardAdmiralsGallery(string Id)
    {
        _cardAdmiralsGalleryRepository.UpdateStatusCardAdmiralsGallery(Id);
    }

    public CardAdmirals SumPowerCardCaptainsGallery()
    {
        return _cardAdmiralsGalleryRepository.SumPowerCardCaptainsGallery();
    }
}
