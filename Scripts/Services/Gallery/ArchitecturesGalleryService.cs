using System.Collections.Generic;

public class ArchitecturesGalleryService : IArchitecturesGalleryService
{
    private readonly IArchitecturesGalleryRepository _ArchitecturesGalleryRepository;

    public ArchitecturesGalleryService(IArchitecturesGalleryRepository ArchitecturesGalleryRepository)
    {
        _ArchitecturesGalleryRepository = ArchitecturesGalleryRepository;
    }

    public static ArchitecturesGalleryService Create()
    {
        return new ArchitecturesGalleryService(new ArchitecturesGalleryRepository());
    }

    public List<Architectures> GetArchitecturesCollection(int pageSize, int offset, string rare)
    {
        List<Architectures> list = _ArchitecturesGalleryRepository.GetArchitecturesCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArchitecturesCount(string rare)
    {
        return _ArchitecturesGalleryRepository.GetArchitecturesCount(rare);
    }

    public void InsertArchitecturesGallery(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        _ArchitecturesGalleryRepository.InsertArchitecturesGallery(Id, _service.GetArchitecturesById(Id));
    }

    public void UpdateStatusArchitecturesGallery(string Id)
    {
        _ArchitecturesGalleryRepository.UpdateStatusArchitecturesGallery(Id);
    }

    public Architectures SumPowerArchitecturesGallery()
    {
        return _ArchitecturesGalleryRepository.SumPowerArchitecturesGallery();
    }

    public void UpdateStarArchitecturesGallery(string Id, double star)
    {
        _ArchitecturesGalleryRepository.UpdateStarArchitecturesGallery(Id, star);
    }

    public void UpdateArchitecturesGalleryPower(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        _ArchitecturesGalleryRepository.UpdateArchitecturesGalleryPower(Id, _service.GetArchitecturesById(Id));
    }
}
