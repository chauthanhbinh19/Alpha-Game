using System.Threading.Tasks;

public interface IAnimesService
{
    Task<Animes> GetAnimeByIdAsync(string id);
    Task<InsertOrUpdateResult<Animes>> InsertAnimeAsync(Animes anime);
    Task<InsertOrUpdateResult<Animes>> UpdateAnimeAsync(Animes anime);
}