using System.Threading.Tasks;

public interface IAnimesService
{
    Task<Animes> GetAnimeByIdAsync(string id);
}