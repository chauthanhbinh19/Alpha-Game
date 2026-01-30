using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryService
{
    Task<List<Runes>> GetRunesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string search, string rare);
    Task InsertRuneGalleryAsync(string Id);
    Task UpdateStatusRuneGalleryAsync(string Id);
    Task UpdateStarRuneGalleryAsync(string id, double star);
    Task UpdateRuneGalleryPowerAsync(string id);
    Task<Runes> SumPowerRunesGalleryAsync();
}