using System.Collections.Generic;

public interface IUserBordersRepository
{
    List<Borders> GetUserBorders(string user_id, int pageSize, int offset, string rare);
    int GetUserBordersCount(string user_id, string rare);
    bool InsertUserBorders(Borders borders, string userId);
    bool InsertUserBordersById(Borders borders, string userId);
    Borders GetBordersByUsed(string user_id);
    void UpdateIsUsedBorders(string borderId, string userId, bool is_used);
    Borders SumPowerUserBorders();
}