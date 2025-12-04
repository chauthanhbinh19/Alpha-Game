using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesGalleryService
{
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string type, string rare);
    Task InsertMagicFormationCircleGalleryAsync(string Id);
    Task UpdateStatusMagicFormationCircleGalleryAsync(string Id);
    Task UpdateStarMagicFormationCircleGalleryAsync(string Id, double star);
    Task UpdateMagicFormationCircleGalleryPowerAsync(string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync();
}