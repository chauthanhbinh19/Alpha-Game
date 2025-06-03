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

    public AnimeStats GetAnimeStats(string type, string user_id)
    {
        return _animeStatsRepository.GetAnimeStats(type, user_id);
    }

    public void InsertOrUpdateAnimeStats(AnimeStats animeStats, string type, string user_id)
    {
        _animeStatsRepository.InsertOrUpdateAnimeStats(animeStats, type, user_id);
    }

    public AnimeStats GetSumAnimeStats(string user_id)
    {
        return _animeStatsRepository.GetSumAnimeStats(user_id);
    }
}