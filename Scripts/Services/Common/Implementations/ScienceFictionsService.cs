using System.Collections.Generic;
using System.Threading.Tasks;
public class ScienceFictionsService : IScienceFictionsService
{
    private static ScienceFictionsService _instance;
    private readonly IScienceFictionsRepository _scienceFictionsRepository;

    public ScienceFictionsService(IScienceFictionsRepository scienceFictionsRepository)
    {
        _scienceFictionsRepository = scienceFictionsRepository;
    }

    public static ScienceFictionsService Create()
    {
        if (_instance == null)
        {
            _instance = new ScienceFictionsService(new ScienceFictionsRepository());
        }
        return _instance;
    }

    public async Task<ScienceFictions> GetScienceFictionByIdAsync(string id)
    {
        return await _scienceFictionsRepository.GetScienceFictionByIdAsync(id);
    }
}