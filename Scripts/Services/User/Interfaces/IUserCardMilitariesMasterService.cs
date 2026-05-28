using System.Threading.Tasks;

public interface IUserCardMilitariesMasterService
{
    Task<Master> GetCardMilitaryMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardMilitaryMasterAsync(Master master, string card_id);
    Task<Master> GetSumCardMilitariesMasterAsync(string user_id, string card_id);
}
