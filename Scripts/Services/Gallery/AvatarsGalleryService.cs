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

    public List<Avatars> GetAvatarsCollection(int pageSize, int offset)
    {
        return _avatarsGalleryRepository.GetAvatarsCollection(pageSize, offset);
    }

    public int GetAvatarsCount()
    {
        return _avatarsGalleryRepository.GetAvatarsCount();
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

    public Avatars SumPowerAvatarsGallery()
    {
        return _avatarsGalleryRepository.SumPowerAvatarsGallery();
    }
}