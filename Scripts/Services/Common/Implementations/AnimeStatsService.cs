using System.Threading.Tasks;

public class AnimeStatsService : IAnimeStatsService
{
    private static AnimeStatsService _instance;
    private readonly IAnimeStatsRepository _animeStatsRepository;

    public AnimeStatsService(IAnimeStatsRepository animeStatsRepository)
    {
        _animeStatsRepository = animeStatsRepository;
    }

    public static AnimeStatsService Create()
    {
        if (_instance == null)
        {
            _instance = new AnimeStatsService(new AnimeStatsRepository());
        }
        return _instance;
    }

    public async Task<AnimeStats> GetAnimeStatsAsync(string id, string user_id)
    {
        return await _animeStatsRepository.GetAnimeStatsAsync(id, user_id);
    }

    public async Task InsertOrUpdateAnimeStatsAsync(AnimeStats animeStats, string user_id)
    {
        await _animeStatsRepository.InsertOrUpdateAnimeStatsAsync(animeStats, user_id);
    }

    public async Task<AnimeStats> GetSumAnimeStatsAsync(string user_id)
    {
        return await _animeStatsRepository.GetSumAnimeStatsAsync(user_id);
    }
}