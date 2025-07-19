using System.Collections.Generic;

public class ArtworkService : IArtworkService
{
    private readonly IArtworkRepository _ArtworkRepository;

    public ArtworkService(IArtworkRepository ArtworkRepository)
    {
        _ArtworkRepository = ArtworkRepository;
    }

    public static ArtworkService Create()
    {
        return new ArtworkService(new ArtworkRepository());
    }

    public List<string> GetUniqueArtworkTypes()
    {
        return _ArtworkRepository.GetUniqueArtworkTypes();
    }

    public List<Artwork> GetArtwork(string type, int pageSize, int offset)
    {
        List<Artwork> list = _ArtworkRepository.GetArtwork(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArtworkCount(string type)
    {
        return _ArtworkRepository.GetArtworkCount(type);
    }

    public List<Artwork> GetArtworkWithPrice(string type, int pageSize, int offset)
    {
        List<Artwork> list = _ArtworkRepository.GetArtworkWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArtworkWithPriceCount(string type)
    {
        return _ArtworkRepository.GetArtworkWithPriceCount(type);
    }

    public Artwork GetArtworkById(string Id)
    {
        return _ArtworkRepository.GetArtworkById(Id);
    }

    public Artwork SumPowerArtworkPercent()
    {
        return _ArtworkRepository.SumPowerArtworkPercent();
    }

    public List<string> GetUniqueArtworkId()
    {
        return _ArtworkRepository.GetUniqueArtworkId();
    }
}