using System.Collections.Generic;

public class CoresGalleryService : ICoresGalleryService
{
    private readonly ICoresGalleryRepository _CoresGalleryRepository;

    public CoresGalleryService(ICoresGalleryRepository CoresGalleryRepository)
    {
        _CoresGalleryRepository = CoresGalleryRepository;
    }

    public static CoresGalleryService Create()
    {
        return new CoresGalleryService(new CoresGalleryRepository());
    }

    public List<Cores> GetCoresCollection(int pageSize, int offset, string rare)
    {
        List<Cores> list = _CoresGalleryRepository.GetCoresCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCoresCount(string rare)
    {
        return _CoresGalleryRepository.GetCoresCount(rare);
    }

    public void InsertCoresGallery(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        _CoresGalleryRepository.InsertCoresGallery(Id, _service.GetCoresById(Id));
    }

    public void UpdateStatusCoresGallery(string Id)
    {
        _CoresGalleryRepository.UpdateStatusCoresGallery(Id);
    }

    public Cores SumPowerCoresGallery()
    {
        return _CoresGalleryRepository.SumPowerCoresGallery();
    }

    public void UpdateStarCoresGallery(string Id, double star)
    {
        _CoresGalleryRepository.UpdateStarCoresGallery(Id, star);
    }

    public void UpdateCoresGalleryPower(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        _CoresGalleryRepository.UpdateCoresGalleryPower(Id, _service.GetCoresById(Id));
    }
}
