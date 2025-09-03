using System.Collections.Generic;

public class TitlesGalleryService : ITitlesGalleryService
{
    private readonly ITitlesGalleryRepository _titlesGalleryRepository;

    public TitlesGalleryService(ITitlesGalleryRepository titlesGalleryRepository)
    {
        _titlesGalleryRepository = titlesGalleryRepository;
    }

    public static TitlesGalleryService Create()
    {
        return new TitlesGalleryService(new TitlesGalleryRepository());
    }

    public List<Titles> GetTitlesCollection(int pageSize, int offset, string rare)
    {
        List<Titles> list = _titlesGalleryRepository.GetTitlesCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTitlesCount(string rare)
    {
        return _titlesGalleryRepository.GetTitlesCount(rare);
    }

    public void InsertTitlesGallery(string Id)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        _titlesGalleryRepository.InsertTitlesGallery(Id, _service.GetTitlesById(Id));
    }

    public void UpdateStatusTitlesGallery(string Id)
    {
        _titlesGalleryRepository.UpdateStatusTitlesGallery(Id);
    }

    public Titles SumPowerTitlesGallery()
    {
        return _titlesGalleryRepository.SumPowerTitlesGallery();
    }

    public void UpdateStarTitlesGallery(string Id, double star)
    {
        _titlesGalleryRepository.UpdateStarTitlesGallery(Id, star);
    }

    public void UpdateTitlesGalleryPower(string Id)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        _titlesGalleryRepository.UpdateTitlesGalleryPower(Id, _service.GetTitlesById(Id));
    }
}
