using System.Collections.Generic;

public interface IUserBordersRepository
{
    List<Borders> GetUserBorders(string user_id, int pageSize, int offset, string rare);
    int GetUserBordersCount(string user_id, string rare);
    bool InsertUserBorders(Borders borders);
    bool InsertUserBordersById(string Id, Borders borders);
    Borders GetBordersByUsed(string user_id);
    void UpdateIsUsedBorders(string Id, bool is_used);
    Borders SumPowerUserBorders();
}