using System.Collections.Generic;

public class SpiritCardGalleryService : ISpiritCardGalleryService
{
    private readonly ISpiritCardGalleryRepository _SpiritCardGalleryRepository;

    public SpiritCardGalleryService(ISpiritCardGalleryRepository SpiritCardGalleryRepository)
    {
        _SpiritCardGalleryRepository = SpiritCardGalleryRepository;
    }

    public static SpiritCardGalleryService Create()
    {
        return new SpiritCardGalleryService(new SpiritCardGalleryRepository());
    }

    public List<SpiritCards> GetSpiritCardCollection(string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = _SpiritCardGalleryRepository.GetSpiritCardCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritCardCount(string type, string rare)
    {
        return _SpiritCardGalleryRepository.GetSpiritCardCount(type, rare);
    }

    public void InsertSpiritCardGallery(string Id)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        _SpiritCardGalleryRepository.InsertSpiritCardGallery(Id, _service.GetSpiritCardById(Id));
    }

    public void UpdateStatusSpiritCardGallery(string Id)
    {
        _SpiritCardGalleryRepository.UpdateStatusSpiritCardGallery(Id);
    }

    public SpiritCards SumPowerSpiritCardGallery()
    {
        return _SpiritCardGalleryRepository.SumPowerSpiritCardGallery();
    }

    public void UpdateStarSpiritCardGallery(string Id, double star)
    {
        _SpiritCardGalleryRepository.UpdateStarSpiritCardGallery(Id, star);
    }

    public void UpdateSpiritCardGalleryPower(string Id)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        _SpiritCardGalleryRepository.UpdateSpiritCardGalleryPower(Id, _service.GetSpiritCardById(Id));
    }
}
