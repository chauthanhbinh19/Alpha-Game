using System.Collections.Generic;

public interface IUserCollaborationRepository
{
    List<Collaboration> GetUserCollaboration(string user_id, int pageSize, int offset);
    int GetUserCollaborationCount(string user_id);
    bool InsertUserCollaborations(Collaboration collaboration);
    bool UpdateCollaborationsLevel(Collaboration collaboration, int cardLevel);
    bool UpdateCollaborationsBreakthrough(Collaboration collaboration, int star, int quantity);
    Collaboration GetUserCollaborationsById(string user_id, string Id);
    Collaboration SumPowerUserCollaborations();
}