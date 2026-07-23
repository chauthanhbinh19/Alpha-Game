using System.Collections.Generic;
using System.Threading.Tasks;

public class MechaBeastsGalleryService : IMechaBeastsGalleryService
{
    private static MechaBeastsGalleryService _instance;
    private readonly IMechaBeastsGalleryRepository _mechaBeastsGalleryRepository;

    public MechaBeastsGalleryService(IMechaBeastsGalleryRepository mechaBeastsGalleryRepository)
    {
        _mechaBeastsGalleryRepository = mechaBeastsGalleryRepository;
    }

    public static MechaBeastsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new MechaBeastsGalleryService(new MechaBeastsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _mechaBeastsGalleryRepository.GetMechaBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsGalleryRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task InsertMechaBeastGalleryAsync(string userId, string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.InsertMechaBeastGalleryAsync(userId, Id, await _service.GetMechaBeastByIdAsync(Id));
    }

    public async Task UpdateStatusMechaBeastGalleryAsync(string userId, string Id)
    {
        await _mechaBeastsGalleryRepository.UpdateStatusMechaBeastGalleryAsync(userId, Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId)
    {
        return await _mechaBeastsGalleryRepository.SumPowerMechaBeastsGalleryAsync(userId);
    }

    public async Task UpdateStarMechaBeastGalleryAsync(string userId, string Id, double star)
    {
        await _mechaBeastsGalleryRepository.UpdateStarMechaBeastGalleryAsync(userId, Id, star);
    }

    public async Task UpdateMechaBeastGalleryPowerAsync(string userId, string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.UpdateMechaBeastGalleryPowerAsync(userId, Id, await _service.GetMechaBeastByIdAsync(Id));
    }
}
