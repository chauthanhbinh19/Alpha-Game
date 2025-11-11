using System.Collections.Generic;

public class AvatarsGalleryService : IAvatarsGalleryService
{
    private readonly IAvatarsGalleryRepository _avatarsGalleryRepository;

    public AvatarsGalleryService(IAvatarsGalleryRepository avatarsGalleryRepository)
    {
        _avatarsGalleryRepository = avatarsGalleryRepository;
    }

    public static AvatarsGalleryService Create()
    {
        return new AvatarsGalleryService(new AvatarsGalleryRepository());
    }

    public List<Achievements> GetAvatarsCollection(int pageSize, int offset, string rare)
    {
        return _avatarsGalleryRepository.GetAvatarsCollection(pageSize, offset, rare);
    }

    public int GetAvatarsCount(string rare)
    {
        return _avatarsGalleryRepository.GetAvatarsCount(rare);
    }

    public void InsertAvatarsGallery(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        _avatarsGalleryRepository.InsertAvatarsGallery(Id, _service.GetAvatarsById(Id));
    }

    public void UpdateStatusAvatarsGallery(string Id)
    {
        _avatarsGalleryRepository.UpdateStatusAvatarsGallery(Id);
    }

    public Achievements SumPowerAvatarsGallery()
    {
        return _avatarsGalleryRepository.SumPowerAvatarsGallery();
    }

    public void UpdateStarAvatarsGallery(string Id, double star)
    {
        _avatarsGalleryRepository.UpdateStarAvatarsGallery(Id, star);
    }

    public void UpdateAvatarsGalleryPower(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        _avatarsGalleryRepository.UpdateAvatarsGalleryPower(Id, _service.GetAvatarsById(Id));
    }
}