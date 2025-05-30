using System.Collections.Generic;

public class CardMonstersGalleryService : ICardMonstersGalleryService
{
    private readonly ICardMonstersGalleryRepository _cardMonstersGalleryRepository;

    public CardMonstersGalleryService(ICardMonstersGalleryRepository cardMonstersGalleryRepository)
    {
        _cardMonstersGalleryRepository = cardMonstersGalleryRepository;
    }

    public static CardMonstersGalleryService Create()
    {
        return new CardMonstersGalleryService(new CardMonstersGalleryRepository());
    }

    public List<CardMonsters> GetCardMonstersCollection(string type, int pageSize, int offset)
    {
        List<CardMonsters> list = _cardMonstersGalleryRepository.GetCardMonstersCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMonstersCount(string type)
    {
        return _cardMonstersGalleryRepository.GetCardMonstersCount(type);
    }

    public void InsertCardMonstersGallery(string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        _cardMonstersGalleryRepository.InsertCardMonstersGallery(Id, _service.GetCardMonstersById(Id));
    }

    public void UpdateStatusCardMonstersGallery(string Id)
    {
        _cardMonstersGalleryRepository.UpdateStatusCardMonstersGallery(Id);
    }

    public CardMonsters SumPowerCardMonstersGallery()
    {
        return _cardMonstersGalleryRepository.SumPowerCardMonstersGallery();
    }
}
