using System.Collections.Generic;

public class CollaborationService : ICollaborationService
{
    private readonly ICollaborationRepository _collaborationRepository;

    public CollaborationService(ICollaborationRepository collaborationRepository)
    {
        _collaborationRepository = collaborationRepository;
    }

    public static CollaborationService Create()
    {
        return new CollaborationService(new CollaborationRepository());
    }

    public List<Collaboration> GetCollaboration(int pageSize, int offset)
    {
        List<Collaboration> list = _collaborationRepository.GetCollaboration(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationCount()
    {
        return _collaborationRepository.GetCollaborationCount();
    }

    public List<Collaboration> GetCollaborationWithPrice(int pageSize, int offset)
    {
        List<Collaboration> list = _collaborationRepository.GetCollaborationWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationWithPriceCount()
    {
        return _collaborationRepository.GetCollaborationWithPriceCount();
    }

    public Collaboration GetCollaborationsById(string Id)
    {
        return _collaborationRepository.GetCollaborationsById(Id);
    }

    public Collaboration SumPowerCollaborationsPercent()
    {
        return _collaborationRepository.SumPowerCollaborationsPercent();
    }

    public List<string> GetUniqueCollaborationId()
    {
        return _collaborationRepository.GetUniqueCollaborationId();
    }
}
