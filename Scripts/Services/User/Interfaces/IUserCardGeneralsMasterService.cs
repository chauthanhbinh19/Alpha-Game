using System.Threading.Tasks;

public interface IUserCardGeneralsMasterService
{
    Task<Master> GetCardGeneralMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardGeneralMasterAsync(Master master, string card_id);
    Task<Master> GetSumCardGeneralsMasterAsync(string user_id, string card_id);
}
