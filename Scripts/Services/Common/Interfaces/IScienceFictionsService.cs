using System.Collections.Generic;
using System.Threading.Tasks;
public interface IScienceFictionsService
{ 
    Task<ScienceFictions> GetScienceFictionByIdAsync(string id);
}