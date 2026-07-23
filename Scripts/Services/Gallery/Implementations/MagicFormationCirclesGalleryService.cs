using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesGalleryService : IMagicFormationCirclesGalleryService
{
    private static MagicFormationCirclesGalleryService _instance;
    private readonly IMagicFormationCirclesGalleryRepository _magicFormationCirclesGalleryRepository;

    public MagicFormationCirclesGalleryService(IMagicFormationCirclesGalleryRepository magicFormationCirclesGalleryRepository)
    {
        _magicFormationCirclesGalleryRepository = magicFormationCirclesGalleryRepository;
    }

    public static MagicFormationCirclesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new MagicFormationCirclesGalleryService(new MagicFormationCirclesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare)
    {
        return await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCountAsync(search, type, rare);
    }

    public async Task InsertMagicFormationCircleGalleryAsync(string userId, string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesGalleryRepository.InsertMagicFormationCircleGalleryAsync(userId, Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }

    public async Task UpdateStatusMagicFormationCircleGalleryAsync(string userId, string Id)
    {
        await _magicFormationCirclesGalleryRepository.UpdateStatusMagicFormationCircleGalleryAsync(userId, Id);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId)
    {
        return await _magicFormationCirclesGalleryRepository.SumPowerMagicFormationCirclesGalleryAsync(userId);
    }

    public async Task UpdateStarMagicFormationCircleGalleryAsync(string userId, string Id, double star)
    {
        await _magicFormationCirclesGalleryRepository.UpdateStarMagicFormationCircleGalleryAsync(userId, Id, star);
    }

    public async Task UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesGalleryRepository.UpdateMagicFormationCircleGalleryPowerAsync(userId, Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }
}
