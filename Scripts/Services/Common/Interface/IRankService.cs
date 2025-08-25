using System.Collections.Generic;
public interface IRankService
{
    Rank EnhanceRank(Rank rank, int level, int multiplier = 1);
    void UpLevel(object data, Rank rank, string type);
}
