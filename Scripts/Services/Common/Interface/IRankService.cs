using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRankService
{
    Rank EnhanceRank(Rank rank, int level, int multiplier = 1);
    Task UpLevelAsync(object data, Rank rank, string type);
}
