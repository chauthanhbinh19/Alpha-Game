using System.Threading.Tasks;

public interface IUserCardMilitariesMasterRepository
{
    Task<Master> GetCardMilitaryMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardMilitaryMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardMilitariesMasterAsync(string user_id, string card_id);
}
