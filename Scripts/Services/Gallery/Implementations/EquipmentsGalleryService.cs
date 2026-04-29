using System.Collections.Generic;
using System.Threading.Tasks;

public class EquipmentsGalleryService : IEquipmentsGalleryService
{
    private static EquipmentsGalleryService _instance;
    private readonly IEquipmentsGalleryRepository _equipmentsGalleryRepository;

    public EquipmentsGalleryService(IEquipmentsGalleryRepository equipmentsGalleryRepository)
    {
        _equipmentsGalleryRepository = equipmentsGalleryRepository;
    }

    public static EquipmentsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new EquipmentsGalleryService(new EquipmentsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Equipments>> GetEquipmentsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = await _equipmentsGalleryRepository.GetEquipmentsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _equipmentsGalleryRepository.GetEquipmentsCountAsync(search, type, rare);
    }

    public async Task InsertEquipmentGalleryAsync(string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        await _equipmentsGalleryRepository.InsertEquipmentGalleryAsync(Id, await _service.GetEquipmentByIdAsync(Id));
    }

    public async Task UpdateStatusEquipmentGalleryAsync(string Id)
    {
        await _equipmentsGalleryRepository.UpdateStatusEquipmentGalleryAsync(Id);
    }

    public async Task<Equipments> SumPowerEquipmentsGalleryAsync()
    {
        return await _equipmentsGalleryRepository.SumPowerEquipmentsGalleryAsync();
    }

    public async Task UpdateStarEquipmentGalleryAsync(string Id, double star)
    {
        await _equipmentsGalleryRepository.UpdateStarEquipmentGalleryAsync(Id, star);
    }

    public async Task UpdateEquipmentGalleryPowerAsync(string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        await _equipmentsGalleryRepository.UpdateEquipmentGalleryPowerAsync(Id, await _service.GetEquipmentByIdAsync(Id));
    }
}
