using System.Collections.Generic;

public class RunesGalleryService : IRunesGalleryService
{
    private readonly IRunesGalleryRepository _RunesGalleryRepository;

    public RunesGalleryService(IRunesGalleryRepository RunesGalleryRepository)
    {
        _RunesGalleryRepository = RunesGalleryRepository;
    }

    public static RunesGalleryService Create()
    {
        return new RunesGalleryService(new RunesGalleryRepository());
    }

    public List<Runes> GetRunesCollection(int pageSize, int offset, string rare)
    {
        List<Runes> list = _RunesGalleryRepository.GetRunesCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRunesCount(string rare)
    {
        return _RunesGalleryRepository.GetRunesCount(rare);
    }

    public void InsertRunesGallery(string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        _RunesGalleryRepository.InsertRunesGallery(Id, _service.GetRunesById(Id));
    }

    public void UpdateStatusRunesGallery(string Id)
    {
        _RunesGalleryRepository.UpdateStatusRunesGallery(Id);
    }

    public Runes SumPowerRunesGallery()
    {
        return _RunesGalleryRepository.SumPowerRunesGallery();
    }

    public void UpdateStarRunesGallery(string Id, double star)
    {
        _RunesGalleryRepository.UpdateStarRunesGallery(Id, star);
    }

    public void UpdateRunesGalleryPower(string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        _RunesGalleryRepository.UpdateRunesGalleryPower(Id, _service.GetRunesById(Id));
    }
}
