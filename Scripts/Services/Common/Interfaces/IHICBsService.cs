using System.Threading.Tasks;

public interface IHICBsService
{
    Task<HICBs> GetHICBByIdAsync(string id);
    Task<InsertOrUpdateResult<HICBs>> InsertHICBAsync(HICBs hicb);
    Task<InsertOrUpdateResult<HICBs>> UpdateHICBAsync(HICBs hicb);
}