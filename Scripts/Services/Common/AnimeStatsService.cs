using System.Threading.Tasks;

public class AnimeStatsService : IAnimeStatsService
{
    private readonly IAnimeStatsRepository _animeStatsRepository;

    public AnimeStatsService(IAnimeStatsRepository animeStatsRepository)
    {
        _animeStatsRepository = animeStatsRepository;
    }

    public static AnimeStatsService Create()
    {
        return new AnimeStatsService(new AnimeStatsRepository());
    }

    public async Task<AnimeStats> GetAnimeStatsAsync(string type, string user_id)
    {
        return await _animeStatsRepository.GetAnimeStatsAsync(type, user_id);
    }

    public async Task InsertOrUpdateAnimeStatsAsync(AnimeStats animeStats, string type, string user_id)
    {
        await _animeStatsRepository.InsertOrUpdateAnimeStatsAsync(animeStats, type, user_id);
    }

    public async Task<AnimeStats> GetSumAnimeStatsAsync(string user_id)
    {
        return await _animeStatsRepository.GetSumAnimeStatsAsync(user_id);
    }
}