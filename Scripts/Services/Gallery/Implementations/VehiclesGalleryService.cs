using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class VehiclesGalleryService : IVehiclesGalleryService
{
    private readonly IVehiclesGalleryRepository _vehiclesGalleryRepository;
    private readonly IVehiclesService _vehiclesService;
    private readonly IPowerManagerService _powerManagerService;

    public VehiclesGalleryService(
        IVehiclesGalleryRepository vehiclesGalleryRepository,
        IVehiclesService vehiclesService,
        IPowerManagerService powerManagerService)
    {
        _vehiclesGalleryRepository = vehiclesGalleryRepository;
        _vehiclesService = vehiclesService;
        _powerManagerService = powerManagerService;
    }

    public static IVehiclesGalleryService Create() => ServiceContainer.GetService<IVehiclesGalleryService>();

    public async Task<List<Vehicles>> GetVehiclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _vehiclesGalleryRepository.GetVehiclesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string search, string type, string rare)
    {
        return await _vehiclesGalleryRepository.GetVehiclesCountAsync(search, type, rare);
    }

    public async Task<bool> InsertVehicleGalleryAsync(string userId, string Id)
    {
        var insertResult = await _vehiclesGalleryRepository.InsertVehicleGalleryAsync(userId, Id, await _vehiclesService.GetVehicleByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusVehicleGalleryAsync(string userId, string vehicleId)
    {
        var updateResult = await _vehiclesGalleryRepository.UpdateStatusVehicleGalleryAsync(userId, vehicleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Vehicles vehicleGallery = await GetVehicleCollectionByIdAsync(userId, vehicleId) ?? new Vehicles();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)vehicleGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusVehiclesGalleryAsync(string userId)
    {
        Vehicles oldVehicle = await SumPowerVehiclesGalleryAsync(userId);

        var updateResult = await _vehiclesGalleryRepository.UpdateBatchStatusVehiclesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Vehicles newVehicle = await SumPowerVehiclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newVehicle - (PowerManager)oldVehicle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Vehicles> SumPowerVehiclesGalleryAsync(string userId)
    {
        return await _vehiclesGalleryRepository.SumPowerVehiclesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarVehicleGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _vehiclesGalleryRepository.UpdateStarVehicleGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarVehicleGalleryAsync(string userId, string vehicleId)
    {
        Vehicles oldVehicle = await GetVehicleCollectionByIdAsync(userId, vehicleId) ?? new Vehicles();

        var updateResult = await _vehiclesGalleryRepository.UpdateCurrentStarVehicleGalleryAsync(userId, vehicleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Vehicles newVehicle = await GetVehicleCollectionByIdAsync(userId, vehicleId) ?? new Vehicles();
        PowerManager deltaPower = (PowerManager)newVehicle - (PowerManager)oldVehicle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarVehiclesGalleryAsync(string userId)
    {
        Vehicles oldVehicle = await SumPowerVehiclesGalleryAsync(userId);

        var updateResult = await _vehiclesGalleryRepository.UpdateBatchCurrentStarVehiclesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Vehicles newVehicle = await SumPowerVehiclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newVehicle - (PowerManager)oldVehicle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchVehiclesGalleryAsync(string userId, List<Vehicles> vehicles)
    {
        var insertResult = await _vehiclesGalleryRepository.InsertBatchVehiclesGalleryAsync(userId, vehicles);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Vehicles> GetVehicleCollectionByIdAsync(string userId, string vehicleId)
    {
        var result = await _vehiclesGalleryRepository.GetVehicleCollectionByIdAsync(userId, vehicleId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateVehicleGalleryPowerAsync(string userId, string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        await _vehiclesGalleryRepository.UpdateVehicleGalleryPowerAsync(userId, Id, await _service.GetVehicleByIdAsync(Id));
    }
}
