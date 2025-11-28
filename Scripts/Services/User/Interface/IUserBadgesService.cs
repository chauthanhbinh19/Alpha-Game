using System.Collections.Generic;

public interface IUserBadgesService
{
    Badges GetNewLevelPower(Badges c, double coefficient);
    Badges GetNewBreakthroughPower(Badges c, double coefficient);
    List<Badges> GetUserBadges(string user_id, int pageSize, int offset, string rare);
    int GetUserBadgesCount(string user_id, string rare);
    bool InsertUserBadges(Badges Badges, string userId);
    bool UpdateBadgesLevel(Badges Badges, int cardLevel);
    bool UpdateBadgesBreakthrough(Badges Badges, int star, double quantity);
    Badges GetUserBadgesById(string user_id, string Id);
    Badges SumPowerUserBadges();
}