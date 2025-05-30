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

    public List<Talisman> GetTalismanCollection(string type, int pageSize, int offset)
    {
        List<Talisman> list = _talismanGalleryRepository.GetTalismanCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTalismanCount(string type)
    {
        return _talismanGalleryRepository.GetTalismanCount(type);
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

    public Talisman SumPowerTalismanGallery()
    {
        return _talismanGalleryRepository.SumPowerTalismanGallery();
    }
}
