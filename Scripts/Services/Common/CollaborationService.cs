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

    public List<Collaborations> GetCollaboration(int pageSize, int offset, string rare)
    {
        List<Collaborations> list = _collaborationRepository.GetCollaboration(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationCount(string rare)
    {
        return _collaborationRepository.GetCollaborationCount(rare);
    }

    public List<Collaborations> GetCollaborationWithPrice(int pageSize, int offset)
    {
        List<Collaborations> list = _collaborationRepository.GetCollaborationWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationWithPriceCount()
    {
        return _collaborationRepository.GetCollaborationWithPriceCount();
    }

    public Collaborations GetCollaborationsById(string Id)
    {
        return _collaborationRepository.GetCollaborationsById(Id);
    }

    public Collaborations SumPowerCollaborationsPercent()
    {
        return _collaborationRepository.SumPowerCollaborationsPercent();
    }

    public List<string> GetUniqueCollaborationId()
    {
        return _collaborationRepository.GetUniqueCollaborationId();
    }
}
