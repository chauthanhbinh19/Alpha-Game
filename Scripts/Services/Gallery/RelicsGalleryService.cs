using System.Collections.Generic;

public class RelicsGalleryService : IRelicsGalleryService
{
    private readonly IRelicsGalleryRepository _relicsGalleryRepository;

    public RelicsGalleryService(IRelicsGalleryRepository relicsGalleryRepository)
    {
        _relicsGalleryRepository = relicsGalleryRepository;
    }

    public static RelicsGalleryService Create()
    {
        return new RelicsGalleryService(new RelicsGalleryRepository());
    }

    public List<Relics> GetRelicsCollection(string type, int pageSize, int offset)
    {
        List<Relics> list = _relicsGalleryRepository.GetRelicsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRelicsCount(string type)
    {
        return _relicsGalleryRepository.GetRelicsCount(type);
    }

    public void InsertRelicsGallery(string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        _relicsGalleryRepository.InsertRelicsGallery(Id, _service.GetRelicsById(Id));
    }

    public void UpdateStatusRelicsGallery(string Id)
    {
        _relicsGalleryRepository.UpdateStatusRelicsGallery(Id);
    }

    public Relics SumPowerRelicsGallery()
    {
        return _relicsGalleryRepository.SumPowerRelicsGallery();
    }
}
