using System.Collections.Generic;

public class VehicleGalleryService : IVehicleGalleryService
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

    public List<Vehicles> GetVehicleCollection(string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = _VehicleGalleryRepository.GetVehicleCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetVehicleCount(string type, string rare)
    {
        return _VehicleGalleryRepository.GetVehicleCount(type, rare);
    }

    public void InsertVehicleGallery(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        _VehicleGalleryRepository.InsertVehicleGallery(Id, _service.GetVehicleById(Id));
    }

    public void UpdateStatusVehicleGallery(string Id)
    {
        _VehicleGalleryRepository.UpdateStatusVehicleGallery(Id);
    }

    public Vehicles SumPowerVehicleGallery()
    {
        return _VehicleGalleryRepository.SumPowerVehicleGallery();
    }

    public void UpdateStarVehicleGallery(string Id, double star)
    {
        _VehicleGalleryRepository.UpdateStarVehicleGallery(Id, star);
    }

    public void UpdateVehicleGalleryPower(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        _VehicleGalleryRepository.UpdateVehicleGalleryPower(Id, _service.GetVehicleById(Id));
    }
}
