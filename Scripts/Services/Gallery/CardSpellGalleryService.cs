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

    public List<CardSpells> GetCardSpellCollection(string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = _cardSpellGalleryRepository.GetCardSpellCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellCount(string type, string rare)
    {
        return _cardSpellGalleryRepository.GetCardSpellCount(type, rare);
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

    public CardSpells SumPowerCardSpellGallery()
    {
        return _cardSpellGalleryRepository.SumPowerCardSpellGallery();
    }

    public void UpdateStarCardSpellGallery(string Id, double star)
    {
        _cardSpellGalleryRepository.UpdateStarCardSpellGallery(Id, star);
    }

    public void UpdateCardSpellGalleryPower(string Id)
    {
        ICardSpellRepository _repository = new CardSpellRepository();
        CardSpellService _service = new CardSpellService(_repository);
        _cardSpellGalleryRepository.UpdateCardSpellGalleryPower(Id, _service.GetCardSpellById(Id));
    }
}
