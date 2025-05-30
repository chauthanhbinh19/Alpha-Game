using System.Collections.Generic;

public class CardSpellGalleryService : ICardSpellGalleryService
{
    private readonly ICardSpellGalleryRepository _cardSpellGalleryRepository;

    public CardSpellGalleryService(ICardSpellGalleryRepository cardSpellGalleryRepository)
    {
        _cardSpellGalleryRepository = cardSpellGalleryRepository;
    }

    public static CardSpellGalleryService Create()
    {
        return new CardSpellGalleryService(new CardSpellGalleryRepository());
    }

    public List<CardSpell> GetCardSpellCollection(string type, int pageSize, int offset)
    {
        List<CardSpell> list = _cardSpellGalleryRepository.GetCardSpellCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellCount(string type)
    {
        return _cardSpellGalleryRepository.GetCardSpellCount(type);
    }

    public void InsertCardSpellGallery(string Id)
    {
        ICardSpellRepository _repository = new CardSpellRepository();
        CardSpellService _service = new CardSpellService(_repository);
        _cardSpellGalleryRepository.InsertCardSpellGallery(Id, _service.GetCardSpellById(Id));
    }

    public void UpdateStatusCardSpellGallery(string Id)
    {
        _cardSpellGalleryRepository.UpdateStatusCardSpellGallery(Id);
    }

    public CardSpell SumPowerCardSpellGallery()
    {
        return _cardSpellGalleryRepository.SumPowerCardSpellGallery();
    }
}
