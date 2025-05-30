using System.Collections.Generic;
public interface IRankService
{
    Rank EnhanceRank(Rank rank, int level);
    void UpLevel(object data, Rank rank, string type);
}
