using System.Threading.Tasks;

public interface IHIENsService
{
    Task<HIENs> GetHIENByIdAsync(string id);
    Task<InsertOrUpdateResult<HIENs>> InsertHIENAsync(HIENs hien);
    Task<InsertOrUpdateResult<HIENs>> UpdateHIENAsync(HIENs hien);
}