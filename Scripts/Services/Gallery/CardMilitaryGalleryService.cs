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

    public List<CardMilitaries> GetCardMilitaryCollection(string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = _cardMilitaryGalleryRepository.GetCardMilitaryCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMilitaryCount(string type, string rare)
    {
        return _cardMilitaryGalleryRepository.GetCardMilitaryCount(type, rare);
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

    public CardMilitaries SumPowerCardMilitaryGallery()
    {
        return _cardMilitaryGalleryRepository.SumPowerCardMilitaryGallery();
    }

    public void UpdateStarCardMilitaryGallery(string Id, double star)
    {
        _cardMilitaryGalleryRepository.UpdateStarCardMilitaryGallery(Id, star);
    }

    public void UpdateCardMilitaryGalleryPower(string Id)
    {
        ICardMilitaryRepository _repository = new CardMilitaryRepository();
        CardMilitaryService _service = new CardMilitaryService(_repository);
        _cardMilitaryGalleryRepository.UpdateCardMilitaryGalleryPower(Id, _service.GetCardMilitaryById(Id));
    }
}
