using System.Collections.Generic;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private readonly ITechnologiesGalleryRepository _TechnologiesGalleryRepository;

    public TechnologiesGalleryService(ITechnologiesGalleryRepository TechnologiesGalleryRepository)
    {
        _TechnologiesGalleryRepository = TechnologiesGalleryRepository;
    }

    public static TechnologiesGalleryService Create()
    {
        return new TechnologiesGalleryService(new TechnologiesGalleryRepository());
    }

    public List<Technologies> GetTechnologiesCollection(int pageSize, int offset, string rare)
    {
        List<Technologies> list = _TechnologiesGalleryRepository.GetTechnologiesCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTechnologiesCount(string rare)
    {
        return _TechnologiesGalleryRepository.GetTechnologiesCount(rare);
    }

    public void InsertTechnologiesGallery(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        _TechnologiesGalleryRepository.InsertTechnologiesGallery(Id, _service.GetTechnologiesById(Id));
    }

    public void UpdateStatusTechnologiesGallery(string Id)
    {
        _TechnologiesGalleryRepository.UpdateStatusTechnologiesGallery(Id);
    }

    public Technologies SumPowerTechnologiesGallery()
    {
        return _TechnologiesGalleryRepository.SumPowerTechnologiesGallery();
    }

    public void UpdateStarTechnologiesGallery(string Id, double star)
    {
        _TechnologiesGalleryRepository.UpdateStarTechnologiesGallery(Id, star);
    }

    public void UpdateTechnologiesGalleryPower(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        _TechnologiesGalleryRepository.UpdateTechnologiesGalleryPower(Id, _service.GetTechnologiesById(Id));
    }
}
