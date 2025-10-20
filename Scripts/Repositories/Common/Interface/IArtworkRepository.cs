using System.Collections.Generic;

public interface IArtworkRepository
{
    List<string> GetUniqueArtworkTypes();
    List<string> GetUniqueArtworkId();
    List<Artworks> GetArtwork(string type, int pageSize, int offset, string rare);
    int GetArtworkCount(string type, string rare);
    List<Artworks> GetArtworkWithPrice(string type, int pageSize, int offset);
    int GetArtworkWithPriceCount(string type);
    Artworks GetArtworkById(string Id);
    Artworks SumPowerArtworkPercent();
}