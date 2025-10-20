using System.Collections.Generic;

public class TalismanGalleryService : ITalismanGalleryService
{
    private readonly ITalismanGalleryRepository _talismanGalleryRepository;

    public TalismanGalleryService(ITalismanGalleryRepository talismanGalleryRepository)
    {
        _talismanGalleryRepository = talismanGalleryRepository;
    }

    public static TalismanGalleryService Create()
    {
        return new TalismanGalleryService(new TalismanGalleryRepository());
    }

    public List<Talismans> GetTalismanCollection(string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = _talismanGalleryRepository.GetTalismanCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTalismanCount(string type, string rare)
    {
        return _talismanGalleryRepository.GetTalismanCount(type, rare);
    }

    public void InsertTalismanGallery(string Id)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        _talismanGalleryRepository.InsertTalismanGallery(Id, _service.GetTalismanById(Id));
    }

    public void UpdateStatusTalismanGallery(string Id)
    {
        _talismanGalleryRepository.UpdateStatusTalismanGallery(Id);
    }

    public Talismans SumPowerTalismanGallery()
    {
        return _talismanGalleryRepository.SumPowerTalismanGallery();
    }

    public void UpdateStarTalismanGallery(string Id, double star)
    {
        _talismanGalleryRepository.UpdateStarTalismanGallery(Id, star);
    }

    public void UpdateTalismanGalleryPower(string Id)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        _talismanGalleryRepository.UpdateTalismanGalleryPower(Id, _service.GetTalismanById(Id));
    }
}
