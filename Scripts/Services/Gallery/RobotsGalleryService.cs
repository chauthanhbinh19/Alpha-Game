using System.Collections.Generic;

public class RobotsGalleryService : IRobotsGalleryService
{
    private readonly IRobotsGalleryRepository _RobotsGalleryRepository;

    public RobotsGalleryService(IRobotsGalleryRepository RobotsGalleryRepository)
    {
        _RobotsGalleryRepository = RobotsGalleryRepository;
    }

    public static RobotsGalleryService Create()
    {
        return new RobotsGalleryService(new RobotsGalleryRepository());
    }

    public List<Robots> GetRobotsCollection(int pageSize, int offset, string rare)
    {
        List<Robots> list = _RobotsGalleryRepository.GetRobotsCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRobotsCount(string rare)
    {
        return _RobotsGalleryRepository.GetRobotsCount(rare);
    }

    public void InsertRobotsGallery(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        _RobotsGalleryRepository.InsertRobotsGallery(Id, _service.GetRobotsById(Id));
    }

    public void UpdateStatusRobotsGallery(string Id)
    {
        _RobotsGalleryRepository.UpdateStatusRobotsGallery(Id);
    }

    public Robots SumPowerRobotsGallery()
    {
        return _RobotsGalleryRepository.SumPowerRobotsGallery();
    }

    public void UpdateStarRobotsGallery(string Id, double star)
    {
        _RobotsGalleryRepository.UpdateStarRobotsGallery(Id, star);
    }

    public void UpdateRobotsGalleryPower(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        _RobotsGalleryRepository.UpdateRobotsGalleryPower(Id, _service.GetRobotsById(Id));
    }
}
