using System.Collections.Generic;

public class MagicFormationCircleGalleryService : IMagicFormationCircleGalleryService
{
    private IMagicFormationCircleGalleryRepository _magicFormationCircleRepository;

    public MagicFormationCircleGalleryService(IMagicFormationCircleGalleryRepository magicFormationCircleRepository)
    {
        _magicFormationCircleRepository = magicFormationCircleRepository;
    }

    public static MagicFormationCircleGalleryService Create()
    {
        return new MagicFormationCircleGalleryService(new MagicFormationCircleGalleryRepository());
    }

    public List<MagicFormationCircles> GetMagicFormationCircleCollection(string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = _magicFormationCircleRepository.GetMagicFormationCircleCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMagicFormationCircleCount(string type, string rare)
    {
        return _magicFormationCircleRepository.GetMagicFormationCircleCount(type, rare);
    }

    public void InsertMagicFormationCircleGallery(string Id)
    {
        IMagicFormationCircleRepository _repository = new MagicFormationCircleRepository();
        MagicFormationCircleService _service = new MagicFormationCircleService(_repository);
        _magicFormationCircleRepository.InsertMagicFormationCircleGallery(Id, _service.GetMagicFormationCircleById(Id));
    }

    public void UpdateStatusMagicFormationCircleGallery(string Id)
    {
        _magicFormationCircleRepository.UpdateStatusMagicFormationCircleGallery(Id);
    }

    public MagicFormationCircles SumPowerMagicFormationCircleGallery()
    {
        return _magicFormationCircleRepository.SumPowerMagicFormationCircleGallery();
    }

    public void UpdateStarMagicFormationCircleGallery(string Id, double star)
    {
        _magicFormationCircleRepository.UpdateStarMagicFormationCircleGallery(Id, star);
    }

    public void UpdateMagicFormationCircleGalleryPower(string Id)
    {
        IMagicFormationCircleRepository _repository = new MagicFormationCircleRepository();
        MagicFormationCircleService _service = new MagicFormationCircleService(_repository);
        _magicFormationCircleRepository.UpdateMagicFormationCircleGalleryPower(Id, _service.GetMagicFormationCircleById(Id));
    }
}
