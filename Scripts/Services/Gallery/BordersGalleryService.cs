using System.Collections.Generic;

public class BordersGalleryService : IBordersGalleryService
{
    private IBordersGalleryRepository _bordersGalleryRepository;

    public BordersGalleryService(IBordersGalleryRepository bordersGalleryRepository)
    {
        _bordersGalleryRepository = bordersGalleryRepository;
    }

    public static BordersGalleryService Create()
    {
        return new BordersGalleryService(new BordersGalleryRepository());
    }

    public List<Borders> GetBordersCollection(int pageSize, int offset)
    {
        List<Borders> list = _bordersGalleryRepository.GetBordersCollection(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBordersCount()
    {
        return _bordersGalleryRepository.GetBordersCount();
    }

    public void InsertBordersGallery(string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        _bordersGalleryRepository.InsertBordersGallery(Id, _service.GetBordersById(Id));
    }

    public void UpdateStatusBordersGallery(string Id)
    {
        _bordersGalleryRepository.UpdateStatusBordersGallery(Id);
    }

    public Borders SumPowerBordersGallery()
    {
        return _bordersGalleryRepository.SumPowerBordersGallery();
    }
}
