using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryRepository
{
    Task<List<Runes>> GetRunesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string rare);
    Task InsertRuneGalleryAsync(string Id, Runes RuneFromDB);
    Task UpdateStatusRuneGalleryAsync(string Id);
    Task UpdateStarRuneGalleryAsync(string id, double star);
    Task UpdateRuneGalleryPowerAsync(string id, Runes RuneFromDB);
    Task<Runes> SumPowerRunesGalleryAsync();
}