using System.Collections.Generic;

public class WeaponsGalleryService : IWeaponsGalleryService
{
    private readonly IWeaponsGalleryRepository _WeaponsGalleryRepository;

    public WeaponsGalleryService(IWeaponsGalleryRepository WeaponsGalleryRepository)
    {
        _WeaponsGalleryRepository = WeaponsGalleryRepository;
    }

    public static WeaponsGalleryService Create()
    {
        return new WeaponsGalleryService(new WeaponsGalleryRepository());
    }

    public List<Weapons> GetWeaponsCollection(int pageSize, int offset, string rare)
    {
        List<Weapons> list = _WeaponsGalleryRepository.GetWeaponsCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetWeaponsCount(string rare)
    {
        return _WeaponsGalleryRepository.GetWeaponsCount(rare);
    }

    public void InsertWeaponsGallery(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        _WeaponsGalleryRepository.InsertWeaponsGallery(Id, _service.GetWeaponsById(Id));
    }

    public void UpdateStatusWeaponsGallery(string Id)
    {
        _WeaponsGalleryRepository.UpdateStatusWeaponsGallery(Id);
    }

    public Weapons SumPowerWeaponsGallery()
    {
        return _WeaponsGalleryRepository.SumPowerWeaponsGallery();
    }

    public void UpdateStarWeaponsGallery(string Id, double star)
    {
        _WeaponsGalleryRepository.UpdateStarWeaponsGallery(Id, star);
    }

    public void UpdateWeaponsGalleryPower(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        _WeaponsGalleryRepository.UpdateWeaponsGalleryPower(Id, _service.GetWeaponsById(Id));
    }
}
