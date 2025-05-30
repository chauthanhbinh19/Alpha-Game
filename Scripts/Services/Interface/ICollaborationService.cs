using System.Collections.Generic;

public interface ICollaborationService
{
    List<Collaboration> GetCollaboration(int pageSize, int offset);
    int GetCollaborationCount();
    List<Collaboration> GetCollaborationWithPrice(int pageSize, int offset);
    int GetCollaborationWithPriceCount();
    Collaboration GetCollaborationsById(string Id);
    Collaboration SumPowerCollaborationsPercent();
}
