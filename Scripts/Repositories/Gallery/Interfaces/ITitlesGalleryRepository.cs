using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryRepository
{
    Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task InsertTitleGalleryAsync(string userId, string Id, Titles TitleFromDB);
    Task UpdateStatusTitleGalleryAsync(string userId, string Id);
    Task UpdateStarTitleGalleryAsync(string userId, string id, double star);
    Task UpdateTitleGalleryPowerAsync(string userId, string id, Titles TitleFromDB);
    Task<Titles> SumPowerTitlesGalleryAsync(string userId);
}