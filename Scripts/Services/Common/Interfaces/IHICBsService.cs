using System.Threading.Tasks;

public interface IHICBsService
{
    Task<HICBs> GetHICBByIdAsync(string id);
}