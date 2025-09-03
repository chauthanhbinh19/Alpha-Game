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

    public List<Forge> GetForgeCollection(string type, int pageSize, int offset, string rare)
    {
        List<Forge> list = _forgeGalleryRepository.GetForgeCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetForgeCount(string type, string rare)
    {
        return _forgeGalleryRepository.GetForgeCount(type, rare);
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

    public void UpdateStarForgeGallery(string Id, double star)
    {
        _forgeGalleryRepository.UpdateStarForgeGallery(Id, star);
    }

    public void UpdateForgeGalleryPower(string Id)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        _forgeGalleryRepository.UpdateForgeGalleryPower(Id, _service.GetForgeById(Id));
    }
}
