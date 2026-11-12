using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITradingsRepository
{
    Task<Tradings> GetTradingAsync(string codeName, string tradingType, string userId);
    Task<List<Tradings>> GetAllAsync();
    Task<InsertOrUpdateResult<Tradings>> InsertTradingAsync(Tradings model);
    Task<InsertOrUpdateResult<Tradings>> UpdateTradingAsync(Tradings model);
    Task<bool> DeleteAsync(int id);
}