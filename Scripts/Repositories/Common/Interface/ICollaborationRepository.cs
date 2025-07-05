using System.Collections.Generic;

public interface ICollaborationRepository
{
    List<string> GetUniqueCollaborationId();
    List<Collaboration> GetCollaboration(int pageSize, int offset);
    int GetCollaborationCount();
    List<Collaboration> GetCollaborationWithPrice(int pageSize, int offset);
    int GetCollaborationWithPriceCount();
    Collaboration GetCollaborationsById(string Id);
    Collaboration SumPowerCollaborationsPercent();
}
