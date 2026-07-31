using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtworksRepository
{
    Task<List<Artworks>> GetUserArtworksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserArtworksCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Artworks>> InsertOrUpdateUserArtworkAsync(string userId, Artworks artwork);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Artworks>>> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworks);
    Task<InsertOrUpdateResult<bool>> UpdateUserArtworkLevelAsync(string userId, Artworks artwork);
    Task<InsertOrUpdateResult<bool>> UpdateUserArtworkStarAsync(string userId, Artworks artwork);
    Task<Artworks> GetUserArtworkByIdAsync(string userId, string Id);
    Task<Artworks> SumPowerUserArtworksAsync(string userId);
}
