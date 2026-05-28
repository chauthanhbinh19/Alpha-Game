using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersGalleryService
{
    Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);
    Task InsertCardSoldierGalleryAsync(string Id);
    Task UpdateStatusCardSoldierGalleryAsync(string Id);
    Task UpdateStarCardSoldierGalleryAsync(string Id, double star);
    Task UpdateCardSoldierGalleryPowerAsync(string Id);
    Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync();
}