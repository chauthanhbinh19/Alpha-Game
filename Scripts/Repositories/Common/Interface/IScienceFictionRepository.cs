using System.Collections.Generic;
public interface IScienceFictionRepository
{
    ScienceFiction GetScienceFiction(string type);
    void InsertOrUpdateScienceFiction(string userId, ScienceFiction scienceFiction, string type);
    ScienceFiction GetSumScienceFiction(string user_id);
}