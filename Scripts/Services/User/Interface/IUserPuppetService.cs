using System.Collections.Generic;

public interface IUserPuppetService
{
    Puppets GetNewLevelPower(Puppets c, double coefficient);
    Puppets GetNewBreakthroughPower(Puppets c, double coefficient);
    List<Puppets> GetUserPuppet(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserPuppetCount(string user_id, string type, string rare);
    bool InsertUserPuppet(Puppets Puppet);
    bool UpdatePuppetLevel(Puppets Puppet, int cardLevel);
    bool UpdatePuppetBreakthrough(Puppets Puppet, int star, int quantity);
    Puppets GetUserPuppetById(string user_id, string Id);
    Puppets SumPowerUserPuppet();
}