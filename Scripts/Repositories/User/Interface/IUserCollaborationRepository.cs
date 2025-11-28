using System.Collections.Generic;

public interface IUserCollaborationRepository
{
    List<Collaborations> GetUserCollaboration(string user_id, int pageSize, int offset, string rare);
    int GetUserCollaborationCount(string user_id, string rare);
    bool InsertUserCollaborations(Collaborations collaboration, string userId);
    bool UpdateCollaborationsLevel(Collaborations collaboration, int cardLevel);
    bool UpdateCollaborationsBreakthrough(Collaborations collaboration, int star, double quantity);
    Collaborations GetUserCollaborationsById(string user_id, string Id);
    Collaborations SumPowerUserCollaborations();
}