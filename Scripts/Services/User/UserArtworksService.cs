
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtworksService : IUserArtworksService
{
     private static UserArtworksService _instance;
    private IUserArtworksRepository _userArtworksRepository;

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

    public async Task<List<Artworks>> GetUserArtworksAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _userArtworksRepository.GetUserArtworksAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtworksCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userArtworksRepository.GetUserArtworksCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserArtworkAsync(Artworks Artwork, string userId)
    {
        return await _userArtworksRepository.InsertUserArtworkAsync(Artwork, userId);
    }

    public async Task<bool> UpdateArtworkLevelAsync(Artworks Artwork, int cardLevel)
    {
        return await _userArtworksRepository.UpdateArtworkLevelAsync(Artwork, cardLevel);
    }

    public async Task<bool> UpdateArtworkBreakthroughAsync(Artworks Artwork, int star, double quantity)
    {
        return await _userArtworksRepository.UpdateArtworkBreakthroughAsync(Artwork, star, quantity);
    }

    public async Task<Artworks> GetUserArtworkByIdAsync(string user_id, string Id)
    {
        return await _userArtworksRepository.GetUserArtworkByIdAsync(user_id, Id);
    }

    public async Task<Artworks> SumPowerUserArtworksAsync()
    {
        return await _userArtworksRepository.SumPowerUserArtworksAsync();
    }
}
