using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryRepository
{
    Task<List<Titles>> GetTitlesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task InsertTitleGalleryAsync(string Id, Titles TitleFromDB);
    Task UpdateStatusTitleGalleryAsync(string Id);
    Task UpdateStarTitleGalleryAsync(string id, double star);
    Task UpdateTitleGalleryPowerAsync(string id, Titles TitleFromDB);
    Task<Titles> SumPowerTitlesGalleryAsync();
}