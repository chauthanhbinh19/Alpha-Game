using System.Collections.Generic;

public class MechaBeastsGalleryService : IMechaBeastsGalleryService
{
    private readonly IMechaBeastsGalleryRepository _MechaBeastsGalleryRepository;

    public MechaBeastsGalleryService(IMechaBeastsGalleryRepository MechaBeastsGalleryRepository)
    {
        _MechaBeastsGalleryRepository = MechaBeastsGalleryRepository;
    }

    public static MechaBeastsGalleryService Create()
    {
        return new MechaBeastsGalleryService(new MechaBeastsGalleryRepository());
    }

    public List<MechaBeasts> GetMechaBeastsCollection(int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = _MechaBeastsGalleryRepository.GetMechaBeastsCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMechaBeastsCount(string rare)
    {
        return _MechaBeastsGalleryRepository.GetMechaBeastsCount(rare);
    }

    public void InsertMechaBeastsGallery(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        _MechaBeastsGalleryRepository.InsertMechaBeastsGallery(Id, _service.GetMechaBeastsById(Id));
    }

    public void UpdateStatusMechaBeastsGallery(string Id)
    {
        _MechaBeastsGalleryRepository.UpdateStatusMechaBeastsGallery(Id);
    }

    public MechaBeasts SumPowerMechaBeastsGallery()
    {
        return _MechaBeastsGalleryRepository.SumPowerMechaBeastsGallery();
    }

    public void UpdateStarMechaBeastsGallery(string Id, double star)
    {
        _MechaBeastsGalleryRepository.UpdateStarMechaBeastsGallery(Id, star);
    }

    public void UpdateMechaBeastsGalleryPower(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        _MechaBeastsGalleryRepository.UpdateMechaBeastsGalleryPower(Id, _service.GetMechaBeastsById(Id));
    }
}
