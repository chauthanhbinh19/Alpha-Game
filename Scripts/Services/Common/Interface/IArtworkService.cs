using System.Collections.Generic;

public interface IArtworkService
{
    List<string> GetUniqueArtworkTypes();
    List<string> GetUniqueArtworkId();
    List<Artwork> GetArtwork(string type, int pageSize, int offset, string rare);
    int GetArtworkCount(string type, string rare);
    List<Artwork> GetArtworkWithPrice(string type, int pageSize, int offset);
    int GetArtworkWithPriceCount(string type);
    Artwork GetArtworkById(string Id);
    Artwork SumPowerArtworkPercent();
}