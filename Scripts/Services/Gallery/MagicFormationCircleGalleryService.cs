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

    public List<MagicFormationCircle> GetMagicFormationCircleCollection(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> list = _magicFormationCircleRepository.GetMagicFormationCircleCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMagicFormationCircleCount(string type)
    {
        return _magicFormationCircleRepository.GetMagicFormationCircleCount(type);
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

    public MagicFormationCircle SumPowerMagicFormationCircleGallery()
    {
        return _magicFormationCircleRepository.SumPowerMagicFormationCircleGallery();
    }
}
