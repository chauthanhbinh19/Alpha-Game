public interface IAnimeStatsService
{
    AnimeStats GetAnimeStats(string type, string user_id);
    void InsertOrUpdateAnimeStats(AnimeStats animeStats, string type, string user_id);
    AnimeStats GetSumAnimeStats(string user_id);
}