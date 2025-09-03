using System.Collections.Generic;

public class ArtworkGalleryService : IArtworkGalleryService
{
    private IArtworkGalleryRepository _ArtworkGalleryRepository;

    public ArtworkGalleryService(IArtworkGalleryRepository ArtworkGalleryRepository)
    {
        _ArtworkGalleryRepository = ArtworkGalleryRepository;
    }

    public static ArtworkGalleryService Create()
    {
        return new ArtworkGalleryService(new ArtworkGalleryRepository());
    }

    public List<Artwork> GetArtworkCollection(string type, int pageSize, int offset, string rare)
    {
        List<Artwork> list = _ArtworkGalleryRepository.GetArtworkCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArtworkCount(string type, string rare)
    {
        return _ArtworkGalleryRepository.GetArtworkCount(type, rare);
    }

    public void InsertArtworkGallery(string Id)
    {
        IArtworkRepository _repository = new ArtworkRepository();
        ArtworkService _service = new ArtworkService(_repository);
        _ArtworkGalleryRepository.InsertArtworkGallery(Id, _service.GetArtworkById(Id));
    }

    public void UpdateStatusArtworkGallery(string Id)
    {
        _ArtworkGalleryRepository.UpdateStatusArtworkGallery(Id);
    }

    public Artwork SumPowerArtworkGallery()
    {
        return _ArtworkGalleryRepository.SumPowerArtworkGallery();
    }

    public void UpdateStarArtworkGallery(string Id, double star)
    {
        _ArtworkGalleryRepository.UpdateStarArtworkGallery(Id, star);
    }

    public void UpdateArtworkGalleryPower(string Id)
    {
        IArtworkRepository _repository = new ArtworkRepository();
        ArtworkService _service = new ArtworkService(_repository);
        _ArtworkGalleryRepository.UpdateArtworkGalleryPower(Id, _service.GetArtworkById(Id));
    }
}
