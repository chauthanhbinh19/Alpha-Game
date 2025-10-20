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

    public List<CollaborationEquipments> GetCollaborationEquipments(string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = _collaborationEquipmentRepository.GetCollaborationEquipments(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationEquipmentCount(string type, string rare)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentCount(type, rare);
    }

    public List<CollaborationEquipments> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset)
    {
        List<CollaborationEquipments> list = _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationEquipmentsWithPriceCount(string type)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPriceCount(type);
    }

    public CollaborationEquipments GetCollaborationEquipmentsById(string Id)
    {
        return _collaborationEquipmentRepository.GetCollaborationEquipmentsById(Id);
    }

    public List<string> GetUniqueCollaborationEquipmentId()
    {
        return _collaborationEquipmentRepository.GetUniqueCollaborationEquipmentId();
    }
}
