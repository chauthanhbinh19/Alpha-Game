using System.Collections.Generic;

public interface IUserArchitecturesRepository
{
    List<Architectures> GetUserArchitectures(string user_id, int pageSize, int offset, string rare);
    int GetUserArchitecturesCount(string user_id, string rare);
    bool InsertUserArchitectures(Architectures Architectures, string userId);
    bool UpdateArchitecturesLevel(Architectures Architectures, int cardLevel);
    bool UpdateArchitecturesBreakthrough(Architectures Architectures, int star, double quantity);
    Architectures GetUserArchitecturesById(string user_id, string Id);
    Architectures SumPowerUserArchitectures();
}