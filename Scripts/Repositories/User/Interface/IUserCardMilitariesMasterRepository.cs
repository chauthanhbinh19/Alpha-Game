using System.Threading.Tasks;

public interface IUserCardMilitariesMasterRepository
{
    Task<Master> GetCardMilitaryMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardMilitaryMasterAsync(Master Master, string card_id);
    Task<Master> GetSumCardMilitariesMasterAsync(string user_id, string card_id);
}
