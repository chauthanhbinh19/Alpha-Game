using System.Collections.Generic;
using System.Threading.Tasks;
public interface IMasterService
{
    Task UpLevelAsync(object data, Master master, string type);
}
