using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtworksRepository
{
    Task<List<Artworks>> GetUserArtworksAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserArtworksCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserArtworkAsync(Artworks Artwork, string userId);
    Task<bool> UpdateArtworkLevelAsync(Artworks Artwork, int cardLevel);
    Task<bool> UpdateArtworkBreakthroughAsync(Artworks Artwork, int star, double quantity);
    Task<Artworks> GetUserArtworkByIdAsync(string user_id, string Id);
    Task<Artworks> SumPowerUserArtworksAsync();
}
