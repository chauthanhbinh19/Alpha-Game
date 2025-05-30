using System.Collections.Generic;

public class CardGeneralsGalleryService : ICardGeneralsGalleryService
{
    private readonly ICardGeneralsGalleryRepository _cardGeneralsGalleryRepository;

    public CardGeneralsGalleryService(ICardGeneralsGalleryRepository cardGeneralsGalleryRepository)
    {
        _cardGeneralsGalleryRepository = cardGeneralsGalleryRepository;
    }

    public static CardGeneralsGalleryService Create()
    {
        return new CardGeneralsGalleryService(new CardGeneralsGalleryRepository());
    }

    public List<CardGenerals> GetCardGeneralsCollection(string type, int pageSize, int offset)
    {
        List<CardGenerals> list = _cardGeneralsGalleryRepository.GetCardGeneralsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardGeneralsCount(string type)
    {
        return _cardGeneralsGalleryRepository.GetCardGeneralsCount(type);
    }

    public void InsertCardGeneralsGallery(string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        _cardGeneralsGalleryRepository.InsertCardGeneralsGallery(Id, _service.GetCardGeneralsById(Id));
    }

    public void UpdateStatusCardGeneralsGallery(string Id)
    {
        _cardGeneralsGalleryRepository.UpdateStatusCardGeneralsGallery(Id);
    }

    public CardGenerals SumPowerCardGeneralsGallery()
    {
        return _cardGeneralsGalleryRepository.SumPowerCardGeneralsGallery();
    }
}
