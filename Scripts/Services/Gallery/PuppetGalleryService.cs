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

    public List<Puppet> GetPuppetCollection(string type, int pageSize, int offset)
    {
        List<Puppet> list = _puppetGalleryRepository.GetPuppetCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetCount(string type)
    {
        return _puppetGalleryRepository.GetPuppetCount(type);
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

    public Puppet SumPowerPuppetGallery()
    {
        return _puppetGalleryRepository.SumPowerPuppetGallery();
    }
}
