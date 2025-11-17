using System.Collections.Generic;

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

    public List<Vehicles> GetVehiclesCollection(string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = _VehicleGalleryRepository.GetVehiclesCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetVehiclesCount(string type, string rare)
    {
        return _VehicleGalleryRepository.GetVehiclesCount(type, rare);
    }

    public void InsertVehiclesGallery(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        _VehicleGalleryRepository.InsertVehiclesGallery(Id, _service.GetVehicleById(Id));
    }

    public void UpdateStatusVehiclesGallery(string Id)
    {
        _VehicleGalleryRepository.UpdateStatusVehiclesGallery(Id);
    }

    public Vehicles SumPowerVehiclesGallery()
    {
        return _VehicleGalleryRepository.SumPowerVehiclesGallery();
    }

    public void UpdateStarVehiclesGallery(string Id, double star)
    {
        _VehicleGalleryRepository.UpdateStarVehiclesGallery(Id, star);
    }

    public void UpdateVehiclesGalleryPower(string Id)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        _VehicleGalleryRepository.UpdateVehiclesGalleryPower(Id, _service.GetVehicleById(Id));
    }
}
