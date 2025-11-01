using System.Collections.Generic;
public class ScienceFictionService : IScienceFictionService
{
    private readonly IScienceFictionRepository _scienceFictionRepository;

    public ScienceFictionService(IScienceFictionRepository scienceFictionRepository)
    {
        _scienceFictionRepository = scienceFictionRepository;
    }

    public static ScienceFictionService Create()
    {
        return new ScienceFictionService(new ScienceFictionRepository());
    }

    public ScienceFiction GetScienceFiction(string type)
    {
        return _scienceFictionRepository.GetScienceFiction(type);
    }

    public ScienceFiction GetSumScienceFiction(string user_id)
    {
        return _scienceFictionRepository.GetSumScienceFiction(user_id);
    }

    public void InsertOrUpdateScienceFiction(string userId, ScienceFiction scienceFiction, string type)
    {
        _scienceFictionRepository.InsertOrUpdateScienceFiction(userId, scienceFiction, type);
    }
}