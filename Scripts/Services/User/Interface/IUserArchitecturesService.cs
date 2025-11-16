using System.Collections.Generic;

public interface IUserArchitecturesService
{
    Architectures GetNewLevelPower(Architectures c, double coefficient);
    Architectures GetNewBreakthroughPower(Architectures c, double coefficient);
    List<Architectures> GetUserArchitectures(string user_id, int pageSize, int offset, string rare);
    int GetUserArchitecturesCount(string user_id, string rare);
    bool InsertUserArchitectures(Architectures Architectures);
    bool UpdateArchitecturesLevel(Architectures Architectures, int cardLevel);
    bool UpdateArchitecturesBreakthrough(Architectures Architectures, int star, double quantity);
    Architectures GetUserArchitecturesById(string user_id, string Id);
    Architectures SumPowerUserArchitectures();
}