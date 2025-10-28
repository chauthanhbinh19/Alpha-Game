using System.Collections.Generic;

public interface IUserCollaborationService
{
    Collaborations GetNewLevelPower(Collaborations c, double coefficient);
    Collaborations GetNewBreakthroughPower(Collaborations c, double coefficient);
    List<Collaborations> GetUserCollaboration(string user_id, int pageSize, int offset, string rare);
    int GetUserCollaborationCount(string user_id, string rare);
    bool InsertUserCollaborations(Collaborations collaboration);
    bool UpdateCollaborationsLevel(Collaborations collaboration, int cardLevel);
    bool UpdateCollaborationsBreakthrough(Collaborations collaboration, int star, double quantity);
    Collaborations GetUserCollaborationsById(string user_id, string Id);
    Collaborations SumPowerUserCollaborations();
}