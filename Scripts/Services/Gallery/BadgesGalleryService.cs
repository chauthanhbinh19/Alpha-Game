using System.Collections.Generic;

public class BadgesGalleryService : IBadgesGalleryService
{
    private readonly IBadgesGalleryRepository _BadgesGalleryRepository;

    public BadgesGalleryService(IBadgesGalleryRepository BadgesGalleryRepository)
    {
        _BadgesGalleryRepository = BadgesGalleryRepository;
    }

    public static BadgesGalleryService Create()
    {
        return new BadgesGalleryService(new BadgesGalleryRepository());
    }

    public List<Badges> GetBadgesCollection(int pageSize, int offset, string rare)
    {
        List<Badges> list = _BadgesGalleryRepository.GetBadgesCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBadgesCount(string rare)
    {
        return _BadgesGalleryRepository.GetBadgesCount(rare);
    }

    public void InsertBadgesGallery(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        _BadgesGalleryRepository.InsertBadgesGallery(Id, _service.GetBadgesById(Id));
    }

    public void UpdateStatusBadgesGallery(string Id)
    {
        _BadgesGalleryRepository.UpdateStatusBadgesGallery(Id);
    }

    public Badges SumPowerBadgesGallery()
    {
        return _BadgesGalleryRepository.SumPowerBadgesGallery();
    }

    public void UpdateStarBadgesGallery(string Id, double star)
    {
        _BadgesGalleryRepository.UpdateStarBadgesGallery(Id, star);
    }

    public void UpdateBadgesGalleryPower(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        _BadgesGalleryRepository.UpdateBadgesGalleryPower(Id, _service.GetBadgesById(Id));
    }
}
