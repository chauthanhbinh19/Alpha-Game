using System.Threading.Tasks;

public interface IHICBsRepository
{
    Task<HICBs> GetHICBByIdAsync(string id);
    Task<InsertOrUpdateResult<HICBs>> InsertHICBAsync(HICBs hicb);
    Task<InsertOrUpdateResult<HICBs>> UpdateHICBAsync(HICBs hicb);
}