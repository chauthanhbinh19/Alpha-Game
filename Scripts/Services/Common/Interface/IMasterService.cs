using System.Collections.Generic;
using System.Threading.Tasks;
public interface IMasterService
{
    Master EnhanceMaster(Master master, int level, int multiplier = 1);
    Task UpLevelAsync(object data, Master master, string type);
}
