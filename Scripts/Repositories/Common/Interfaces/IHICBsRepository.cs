using System.Threading.Tasks;

public interface IHICBsRepository
{
    Task<HICBs> GetHICBByIdAsync(string id);
}