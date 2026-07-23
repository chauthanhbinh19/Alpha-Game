using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesGalleryRepository
{
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare);
    Task InsertMagicFormationCircleGalleryAsync(string userId, string Id, MagicFormationCircles MagicFormationCircleFromDB);
    Task UpdateStatusMagicFormationCircleGalleryAsync(string userId, string Id);
    Task UpdateStarMagicFormationCircleGalleryAsync(string userId, string Id, double star);
    Task UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id, MagicFormationCircles MagicFormationCircleFromDB);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId);
}