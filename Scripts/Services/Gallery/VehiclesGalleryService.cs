using System.Collections.Generic;
using System.Threading.Tasks;

public class VehiclesGalleryService : IVehiclesGalleryService
{
    private static VehiclesGalleryService _instance;
    private readonly IVehiclesGalleryRepository _vehiclesGalleryRepository;

    public VehiclesGalleryService(IVehiclesGalleryRepository vehiclesGalleryRepository)
    {
        _vehiclesGalleryRepository = vehiclesGalleryRepository;
    }

    public static VehiclesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new VehiclesGalleryService(new VehiclesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Vehicles>> GetVehiclesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _vehiclesGalleryRepository.GetVehiclesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string search, string type, string rare)
    {
        return await _vehiclesGalleryRepository.GetVehiclesCountAsync(search, type, rare);
    }

    public async Task InsertVehicleGalleryAsync(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        await _vehiclesGalleryRepository.InsertVehicleGalleryAsync(Id, await _service.GetVehicleByIdAsync(Id));
    }

    public async Task UpdateStatusVehicleGalleryAsync(string Id)
    {
        await _vehiclesGalleryRepository.UpdateStatusVehicleGalleryAsync(Id);
    }

    public async Task<Vehicles> SumPowerVehiclesGalleryAsync()
    {
        return await _vehiclesGalleryRepository.SumPowerVehiclesGalleryAsync();
    }

    public async Task UpdateStarVehicleGalleryAsync(string Id, double star)
    {
        await _vehiclesGalleryRepository.UpdateStarVehicleGalleryAsync(Id, star);
    }

    public async Task UpdateVehicleGalleryPowerAsync(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        await _vehiclesGalleryRepository.UpdateVehicleGalleryPowerAsync(Id, await _service.GetVehicleByIdAsync(Id));
    }
}
