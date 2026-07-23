
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtworksService : IUserArtworksService
{
    private static UserArtworksService _instance;
    private readonly IUserArtworksRepository _userArtworksRepository;

    public UserArtworksService(IUserArtworksRepository userArtworksRepository)
    {
        _userArtworksRepository = userArtworksRepository;
    }

    public static UserArtworksService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArtworksService(new UserArtworksRepository());
        }
        return _instance;
    }

    public async Task<List<Artworks>> GetUserArtworksAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _userArtworksRepository.GetUserArtworksAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtworksCountAsync(string userId, string search, string type, string rare)
    {
        return await _userArtworksRepository.GetUserArtworksCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserArtworkAsync(Artworks artwork, string userId)
    {
        return await _userArtworksRepository.InsertUserArtworkAsync(artwork, userId);
    }

    public async Task<bool> UpdateUserArtworkLevelAsync(string userId, Artworks artwork)
    {
        return await _userArtworksRepository.UpdateUserArtworkLevelAsync(userId, artwork);
    }

    public async Task<bool> UpdateUserArtworkStarAsync(string userId, Artworks artwork)
    {
        return await _userArtworksRepository.UpdateUserArtworkStarAsync(userId, artwork);
    }

    public async Task<bool> UpdateUserArtworkBreakthroughAsync(string userId, Artworks artwork, int star, double quantity)
    {
        return await _userArtworksRepository.UpdateUserArtworkBreakthroughAsync(userId, artwork, star, quantity);
    }

    public async Task<Artworks> GetUserArtworkByIdAsync(string userId, string Id)
    {
        return await _userArtworksRepository.GetUserArtworkByIdAsync(userId, Id);
    }

    public async Task<Artworks> SumPowerUserArtworksAsync(string userId)
    {
        return await _userArtworksRepository.SumPowerUserArtworksAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworks)
    {
        return await _userArtworksRepository.InsertOrUpdateUserArtworksBatchAsync(userId, artworks);
    }
}
