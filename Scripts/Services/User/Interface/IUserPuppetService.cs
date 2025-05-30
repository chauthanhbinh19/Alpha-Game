using System.Collections.Generic;

public interface IUserPuppetService
{
    Puppet GetNewLevelPower(Puppet c, double coefficient);
    Puppet GetNewBreakthroughPower(Puppet c, double coefficient);
    List<Puppet> GetUserPuppet(string user_id, string type, int pageSize, int offset);
    int GetUserPuppetCount(string user_id, string type);
    bool InsertUserPuppet(Puppet Puppet);
    bool UpdatePuppetLevel(Puppet Puppet, int cardLevel);
    bool UpdatePuppetBreakthrough(Puppet Puppet, int star, int quantity);
    Puppet GetUserPuppetById(string user_id, string Id);
    Puppet SumPowerUserPuppet();
}