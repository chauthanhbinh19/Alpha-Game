using System.Collections.Generic;

public interface ICollaborationService
{
    List<string> GetUniqueCollaborationId();
    List<Collaborations> GetCollaboration(int pageSize, int offset, string rare);
    int GetCollaborationCount(string rare);
    List<Collaborations> GetCollaborationWithPrice(int pageSize, int offset);
    int GetCollaborationWithPriceCount();
    Collaborations GetCollaborationsById(string Id);
    Collaborations SumPowerCollaborationsPercent();
}
