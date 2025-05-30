using System.Collections.Generic;

public class CardColonelsGalleryService : ICardColonelsGalleryService
{
    private ICardColonelsGalleryRepository _cardColonelsGalleryRepository;

    public CardColonelsGalleryService(ICardColonelsGalleryRepository cardColonelsGalleryRepository)
    {
        _cardColonelsGalleryRepository = cardColonelsGalleryRepository;
    }

    public static CardColonelsGalleryService Create()
    {
        return new CardColonelsGalleryService(new CardColonelsGalleryRepository());
    }

    public List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset)
    {
        List<CardColonels> list = _cardColonelsGalleryRepository.GetCardColonelsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardColonelsCount(string type)
    {
        return _cardColonelsGalleryRepository.GetCardColonelsCount(type);
    }

    public void InsertCardColonelsGallery(string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        _cardColonelsGalleryRepository.InsertCardColonelsGallery(Id, _service.GetCardColonelsById(Id));
    }

    public void UpdateStatusCardColonelsGallery(string Id)
    {
        _cardColonelsGalleryRepository.UpdateStatusCardColonelsGallery(Id);
    }

    public CardColonels SumPowerCardColonelsGallery()
    {
        return _cardColonelsGalleryRepository.SumPowerCardColonelsGallery();
    }
}
