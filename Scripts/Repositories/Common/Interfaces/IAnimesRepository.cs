using System.Threading.Tasks;

public interface IAnimesRepository
{
    Task<Animes> GetAnimeByIdAsync(string id);
}