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

    public async Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _mechaBeastsGalleryRepository.GetMechaBeastsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsGalleryRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task InsertMechaBeastGalleryAsync(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.InsertMechaBeastGalleryAsync(Id, await _service.GetMechaBeastByIdAsync(Id));
    }

    public async Task UpdateStatusMechaBeastGalleryAsync(string Id)
    {
        await _mechaBeastsGalleryRepository.UpdateStatusMechaBeastGalleryAsync(Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync()
    {
        return await _mechaBeastsGalleryRepository.SumPowerMechaBeastsGalleryAsync();
    }

    public async Task UpdateStarMechaBeastGalleryAsync(string Id, double star)
    {
        await _mechaBeastsGalleryRepository.UpdateStarMechaBeastGalleryAsync(Id, star);
    }

    public async Task UpdateMechaBeastGalleryPowerAsync(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.UpdateMechaBeastGalleryPowerAsync(Id, await _service.GetMechaBeastByIdAsync(Id));
    }
}
