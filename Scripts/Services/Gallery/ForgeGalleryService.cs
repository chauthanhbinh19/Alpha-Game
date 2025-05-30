using System.Collections.Generic;
using Unity.VisualScripting;

public class ForgeGalleryService : IForgeGalleryService
{
    private IForgeGalleryRepository _forgeGalleryRepository;

    public ForgeGalleryService(IForgeGalleryRepository forgeGalleryRepository)
    {
        _forgeGalleryRepository = forgeGalleryRepository;
    }

    public static ForgeGalleryService Create()
    {
        return new ForgeGalleryService(new ForgeGalleryRepository());
    }

    public List<Forge> GetForgeCollection(string type, int pageSize, int offset)
    {
        List<Forge> list = _forgeGalleryRepository.GetForgeCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetForgeCount(string type)
    {
        return _forgeGalleryRepository.GetForgeCount(type);
    }

    public void InsertForgeGallery(string Id)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        _forgeGalleryRepository.InsertForgeGallery(Id, _service.GetForgeById(Id));
    }

    public void UpdateStatusForgeGallery(string Id)
    {
        _forgeGalleryRepository.UpdateStatusForgeGallery(Id);
    }

    public Forge SumPowerForgeGallery()
    {
        return _forgeGalleryRepository.SumPowerForgeGallery();
    }
}
