using System.Threading.Tasks;

public interface IAnimeStatsService
{
    Task<AnimeStats> GetAnimeStatsAsync(string type, string user_id);
    Task InsertOrUpdateAnimeStatsAsync(AnimeStats animeStats, string type, string user_id);
    Task<AnimeStats> GetSumAnimeStatsAsync(string user_id);
}