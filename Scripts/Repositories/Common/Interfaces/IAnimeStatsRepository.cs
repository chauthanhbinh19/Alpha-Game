using System.Threading.Tasks;

public interface IAnimeStatsRepository
{
    Task<AnimeStats> GetAnimeStatsAsync(string id, string user_id);
    Task InsertOrUpdateAnimeStatsAsync(AnimeStats animeStats, string user_id);
    Task<AnimeStats> GetSumAnimeStatsAsync(string user_id);
}