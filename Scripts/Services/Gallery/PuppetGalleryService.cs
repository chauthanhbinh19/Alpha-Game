using System.Collections.Generic;

public class PuppetGalleryService : IPuppetGalleryService
{
    private readonly IPuppetGalleryRepository _puppetGalleryRepository;

    public PuppetGalleryService(IPuppetGalleryRepository puppetGalleryRepository)
    {
        _puppetGalleryRepository = puppetGalleryRepository;
    }

    public static PuppetGalleryService Create()
    {
        return new PuppetGalleryService(new PuppetGalleryRepository());
    }

    public List<Puppets> GetPuppetCollection(string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = _puppetGalleryRepository.GetPuppetCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetCount(string type, string rare)
    {
        return _puppetGalleryRepository.GetPuppetCount(type, rare);
    }

    public void InsertPuppetGallery(string Id)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        _puppetGalleryRepository.InsertPuppetGallery(Id, _service.GetPuppetById(Id));
    }

    public void UpdateStatusPuppetGallery(string Id)
    {
        _puppetGalleryRepository.UpdateStatusPuppetGallery(Id);
    }

    public Puppets SumPowerPuppetGallery()
    {
        return _puppetGalleryRepository.SumPowerPuppetGallery();
    }

    public void UpdateStarPuppetGallery(string Id, double star)
    {
        _puppetGalleryRepository.UpdateStarPuppetGallery(Id, star);
    }

    public void UpdatePuppetGalleryPower(string Id)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        _puppetGalleryRepository.UpdatePuppetGalleryPower(Id, _service.GetPuppetById(Id));
    }
}
