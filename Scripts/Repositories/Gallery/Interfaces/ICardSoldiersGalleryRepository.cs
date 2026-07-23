using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersGalleryRepository
{
    Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);
    Task InsertCardSoldierGalleryAsync(string userId, string Id, CardSoldiers CardSoldierFromDB);
    Task UpdateStatusCardSoldierGalleryAsync(string userId, string Id);
    Task UpdateStarCardSoldierGalleryAsync(string userId, string Id, double star);
    Task UpdateCardSoldierGalleryPowerAsync(string userId, string Id, CardSoldiers CardSoldierFromDB);
    Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId);
}