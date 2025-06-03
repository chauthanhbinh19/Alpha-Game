using System.Collections.Generic;

public class CollaborationEquipmentService : ICollaborationEquipmentService
{
    private readonly ICollaborationEquipmentRepository _collaborationEquipmentRepository;

    public CollaborationEquipmentService(ICollaborationEquipmentRepository collaborationEquipmentRepository)
    {
        _collaborationEquipmentRepository = collaborationEquipmentRepository;
    }

    public static CollaborationEquipmentService Create()
    {
        return new CollaborationEquipmentService(new CollaborationEquipmentRepository());
    }


    public List<string> GetUniqueCollaborationEquipmentTypes()
    {
        return _collaborationEquipmentRepository.GetUniqueCollaborationEquipmentTypes();
    }

    public List<CollaborationEquipment> GetCollaborationEquipments(string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> list = _collaborationEquipmentRepository.GetCollaborationEquipments(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationEquipmentCount(string type)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentCount(type);
    }

    public List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> list = _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationEquipmentsWithPriceCount(string type)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPriceCount(type);
    }

    public CollaborationEquipment GetCollaborationEquipmentsById(string Id)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentsById(Id);
    }

}
