using System.Collections.Generic;
public interface IMasterService
{
    Master EnhanceMaster(Master master, int level, int multiplier = 1);
    void UpLevel(object data, Master master, string type);
}
