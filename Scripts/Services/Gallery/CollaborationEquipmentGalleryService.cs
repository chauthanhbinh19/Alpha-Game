using System.Collections.Generic;

public class CollaborationEquipmentGalleryService : ICollaborationEquipmentGalleryService
{
    private readonly ICollaborationEquipmentGalleryRepository _collabEquipmentGalleryRepo;

    public CollaborationEquipmentGalleryService(ICollaborationEquipmentGalleryRepository collabEquipmentGalleryRepo)
    {
        _collabEquipmentGalleryRepo = collabEquipmentGalleryRepo;
    }

    public static CollaborationEquipmentGalleryService Create()
    {
        return new CollaborationEquipmentGalleryService(new CollaborationEquipmentGalleryRepository());
    }

    public List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipment> list = _collabEquipmentGalleryRepo.GetCollaborationEquipmentsCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationEquipmentCount(string type, string rare)
    {
        return _collabEquipmentGalleryRepo.GetCollaborationEquipmentCount(type, rare);
    }

    public void InsertCollaborationEquipmentsGallery(string Id)
    {
        ICollaborationEquipmentRepository _repository = new CollaborationEquipmentRepository();
        CollaborationEquipmentService _service = new CollaborationEquipmentService(_repository);
        _collabEquipmentGalleryRepo.InsertCollaborationEquipmentsGallery(Id, _service.GetCollaborationEquipmentsById(Id));
    }

    public void UpdateStatusCollaborationEquipmentsGallery(string Id)
    {
        _collabEquipmentGalleryRepo.UpdateStatusCollaborationEquipmentsGallery(Id);
    }

    public CollaborationEquipment SumPowerCollaborationEquipmentsGallery()
    {
        return _collabEquipmentGalleryRepo.SumPowerCollaborationEquipmentsGallery();
    }
}
