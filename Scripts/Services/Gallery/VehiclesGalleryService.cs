using System.Collections.Generic;
using System.Threading.Tasks;

public class VehiclesGalleryService : IVehiclesGalleryService
{
    private readonly IVehiclesGalleryRepository _VehicleGalleryRepository;

    public VehiclesGalleryService(IVehiclesGalleryRepository VehicleGalleryRepository)
    {
        _VehicleGalleryRepository = VehicleGalleryRepository;
    }

    public static VehiclesGalleryService Create()
    {
        return new VehiclesGalleryService(new VehiclesGalleryRepository());
    }

    public async Task<List<Vehicles>> GetVehiclesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _VehicleGalleryRepository.GetVehiclesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string search, string type, string rare)
    {
        return await _VehicleGalleryRepository.GetVehiclesCountAsync(search, type, rare);
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
