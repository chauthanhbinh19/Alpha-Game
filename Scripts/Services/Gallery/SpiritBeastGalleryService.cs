using System.Collections.Generic;

public class SpiritBeastGalleryService : ISpiritBeastGalleryService
{
    private readonly ISpiritBeastGalleryRepository _SpiritBeastGalleryRepository;

    public SpiritBeastGalleryService(ISpiritBeastGalleryRepository SpiritBeastGalleryRepository)
    {
        _SpiritBeastGalleryRepository = SpiritBeastGalleryRepository;
    }

    public static SpiritBeastGalleryService Create()
    {
        return new SpiritBeastGalleryService(new SpiritBeastGalleryRepository());
    }

    public List<SpiritBeast> GetSpiritBeastCollection(int pageSize, int offset, string rare)
    {
        List<SpiritBeast> list = _SpiritBeastGalleryRepository.GetSpiritBeastCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritBeastCount(string rare)
    {
        return _SpiritBeastGalleryRepository.GetSpiritBeastCount(rare);
    }

    public void InsertSpiritBeastGallery(string Id)
    {
        ISpiritBeastRepository _repository = new SpiritBeastRepository();
        SpiritBeastService _service = new SpiritBeastService(_repository);
        _SpiritBeastGalleryRepository.InsertSpiritBeastGallery(Id, _service.GetSpiritBeastById(Id));
    }

    public void UpdateStatusSpiritBeastGallery(string Id)
    {
        _SpiritBeastGalleryRepository.UpdateStatusSpiritBeastGallery(Id);
    }

    public SpiritBeast SumPowerSpiritBeastGallery()
    {
        return _SpiritBeastGalleryRepository.SumPowerSpiritBeastGallery();
    }
}
