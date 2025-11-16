using System.Collections.Generic;

public class CardsGalleryService : ICardsGalleryService
{
    private readonly ICardsGalleryRepository _CardsGalleryRepository;

    public CardsGalleryService(ICardsGalleryRepository CardsGalleryRepository)
    {
        _CardsGalleryRepository = CardsGalleryRepository;
    }

    public static CardsGalleryService Create()
    {
        return new CardsGalleryService(new CardsGalleryRepository());
    }

    public List<Cards> GetCardsCollection(int pageSize, int offset, string rare)
    {
        List<Cards> list = _CardsGalleryRepository.GetCardsCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardsCount(string rare)
    {
        return _CardsGalleryRepository.GetCardsCount(rare);
    }

    public void InsertCardsGallery(string Id)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        _CardsGalleryRepository.InsertCardsGallery(Id, _service.GetCardsById(Id));
    }

    public void UpdateStatusCardsGallery(string Id)
    {
        _CardsGalleryRepository.UpdateStatusCardsGallery(Id);
    }

    public Cards SumPowerCardsGallery()
    {
        return _CardsGalleryRepository.SumPowerCardsGallery();
    }

    public void UpdateStarCardsGallery(string Id, double star)
    {
        _CardsGalleryRepository.UpdateStarCardsGallery(Id, star);
    }

    public void UpdateCardsGalleryPower(string Id)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        _CardsGalleryRepository.UpdateCardsGalleryPower(Id, _service.GetCardsById(Id));
    }
}
