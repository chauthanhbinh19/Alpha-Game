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

    public List<Artworks> GetArtwork(string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = _ArtworkRepository.GetArtwork(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArtworkCount(string type, string rare)
    {
        return _ArtworkRepository.GetArtworkCount(type, rare);
    }

    public List<Artworks> GetArtworkWithPrice(string type, int pageSize, int offset)
    {
        List<Artworks> list = _ArtworkRepository.GetArtworkWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArtworkWithPriceCount(string type)
    {
        return _ArtworkRepository.GetArtworkWithPriceCount(type);
    }

    public Artworks GetArtworkById(string Id)
    {
        return _ArtworkRepository.GetArtworkById(Id);
    }

    public Artworks SumPowerArtworkPercent()
    {
        return _ArtworkRepository.SumPowerArtworkPercent();
    }

    public List<string> GetUniqueArtworkId()
    {
        return _ArtworkRepository.GetUniqueArtworkId();
    }
}