using System.Collections.Generic;

public class CardMilitaryGalleryService : ICardMilitaryGallerService
{
    private readonly ICardMilitaryGalleryRepository _cardMilitaryGalleryRepository;

    public CardMilitaryGalleryService(ICardMilitaryGalleryRepository cardMilitaryGalleryRepository)
    {
        _cardMilitaryGalleryRepository = cardMilitaryGalleryRepository;
    }

    public static CardMilitaryGalleryService Create()
    {
        return new CardMilitaryGalleryService(new CardMilitaryGalleryRepository());
    }

    public List<CardMilitary> GetCardMilitaryCollection(string type, int pageSize, int offset)
    {
        List<CardMilitary> list = _cardMilitaryGalleryRepository.GetCardMilitaryCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMilitaryCount(string type)
    {
        return _cardMilitaryGalleryRepository.GetCardMilitaryCount(type);
    }

    public void InsertCardMilitaryGallery(string Id)
    {
        ICardMilitaryRepository _repository = new CardMilitaryRepository();
        CardMilitaryService _service = new CardMilitaryService(_repository);
        _cardMilitaryGalleryRepository.InsertCardMilitaryGallery(Id, _service.GetCardMilitaryById(Id));
    }

    public void UpdateStatusCardMilitaryGallery(string Id)
    {
        _cardMilitaryGalleryRepository.UpdateStatusCardMilitaryGallery(Id);
    }

    public CardMilitary SumPowerCardMilitaryGallery()
    {
        return _cardMilitaryGalleryRepository.SumPowerCardMilitaryGallery();
    }
}
