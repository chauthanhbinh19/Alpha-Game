using System.Collections.Generic;
public interface IScienceFictionService
{ 
    ScienceFiction GetScienceFiction(string type);
    void InsertOrUpdateScienceFiction(ScienceFiction scienceFiction, string type);
    ScienceFiction GetSumScienceFiction(string user_id);
}