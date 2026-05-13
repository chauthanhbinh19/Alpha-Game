using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRankService
{
    Task UpLevelAsync(object data, Rank rank, string type);
}
