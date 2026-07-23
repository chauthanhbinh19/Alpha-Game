using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsGalleryService : IWeaponsGalleryService
{
    private static WeaponsGalleryService _instance;
    private readonly IWeaponsGalleryRepository _weaponsGalleryRepository;

    public WeaponsGalleryService(IWeaponsGalleryRepository weaponsGalleryRepository)
    {
        _weaponsGalleryRepository = weaponsGalleryRepository;
    }

    public static WeaponsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new WeaponsGalleryService(new WeaponsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _weaponsGalleryRepository.GetWeaponsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string type, string rare)
    {
        return await _weaponsGalleryRepository.GetWeaponsCountAsync(search, type, rare);
    }

    public async Task InsertWeaponGalleryAsync(string userId, string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _weaponsGalleryRepository.InsertWeaponGalleryAsync(userId, Id, await _service.GetWeaponByIdAsync(Id));
    }

    public async Task UpdateStatusWeaponGalleryAsync(string userId, string Id)
    {
        await _weaponsGalleryRepository.UpdateStatusWeaponGalleryAsync(userId, Id);
    }

    public async Task<Weapons> SumPowerWeaponsGalleryAsync(string userId)
    {
        return await _weaponsGalleryRepository.SumPowerWeaponsGalleryAsync(userId);
    }

    public async Task UpdateStarWeaponGalleryAsync(string userId, string Id, double star)
    {
        await _weaponsGalleryRepository.UpdateStarWeaponGalleryAsync(userId, Id, star);
    }

    public async Task UpdateWeaponGalleryPowerAsync(string userId, string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _weaponsGalleryRepository.UpdateWeaponGalleryPowerAsync(userId, Id, await _service.GetWeaponByIdAsync(Id));
    }
}
