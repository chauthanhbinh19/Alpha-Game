using System.Collections.Generic;

public class AlchemyGalleryService : IAlchemyGalleryService
{
    private IAlchemyGalleryRepository _alchemyGalleryRepository;

    public AlchemyGalleryService(IAlchemyGalleryRepository alchemyGalleryRepository)
    {
        _alchemyGalleryRepository = alchemyGalleryRepository;
    }

    public static AlchemyGalleryService Create()
    {
        return new AlchemyGalleryService(new AlchemyGalleryRepository());
    }

    public List<Alchemy> GetAlchemyCollection(string type, int pageSize, int offset)
    {
        List<Alchemy> list = _alchemyGalleryRepository.GetAlchemyCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAlchemyCount(string type)
    {
        return _alchemyGalleryRepository.GetAlchemyCount(type);
    }

    public void InsertAlchemyGallery(string Id)
    {
        IAlchemyRepository _repository = new AlchemyRepository();
        AlchemyService _service = new AlchemyService(_repository);
        _alchemyGalleryRepository.InsertAlchemyGallery(Id, _service.GetAlchemyById(Id));
    }

    public void UpdateStatusAlchemyGallery(string Id)
    {
        _alchemyGalleryRepository.UpdateStatusAlchemyGallery(Id);
    }

    public Alchemy SumPowerAlchemyGallery()
    {
        return _alchemyGalleryRepository.SumPowerAlchemyGallery();
    }
}
