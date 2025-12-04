using System.Collections.Generic;
using System.Threading.Tasks;

public class VehicleGalleryService : IVehiclesGalleryService
{
    private readonly IVehicleGalleryRepository _VehicleGalleryRepository;

    public VehicleGalleryService(IVehicleGalleryRepository VehicleGalleryRepository)
    {
        _VehicleGalleryRepository = VehicleGalleryRepository;
    }

    public static VehicleGalleryService Create()
    {
        return new VehicleGalleryService(new VehicleGalleryRepository());
    }

    public async Task<List<Vehicles>> GetVehiclesCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _VehicleGalleryRepository.GetVehiclesCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string type, string rare)
    {
        return await _VehicleGalleryRepository.GetVehiclesCountAsync(type, rare);
    }

    public async Task InsertVehicleGalleryAsync(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        await _VehicleGalleryRepository.InsertVehicleGalleryAsync(Id, await _service.GetVehicleByIdAsync(Id));
    }

    public async Task UpdateStatusVehicleGalleryAsync(string Id)
    {
        await _VehicleGalleryRepository.UpdateStatusVehicleGalleryAsync(Id);
    }

    public async Task<Vehicles> SumPowerVehiclesGalleryAsync()
    {
        return await _VehicleGalleryRepository.SumPowerVehiclesGalleryAsync();
    }

    public async Task UpdateStarVehicleGalleryAsync(string Id, double star)
    {
        await _VehicleGalleryRepository.UpdateStarVehicleGalleryAsync(Id, star);
    }

    public async Task UpdateVehicleGalleryPowerAsync(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        await _VehicleGalleryRepository.UpdateVehicleGalleryPowerAsync(Id, await _service.GetVehicleByIdAsync(Id));
    }
}
