using System.Collections.Generic;
using System.Threading.Tasks;
public class ScienceFictionsService : IScienceFictionsService
{
    private readonly IScienceFictionsRepository _scienceFictionsRepository;

    public ScienceFictionsService(IScienceFictionsRepository scienceFictionsRepository)
    {
        _scienceFictionsRepository = scienceFictionsRepository;
    }

    public static IScienceFictionsService Create() => ServiceContainer.GetService<IScienceFictionsService>();

    public async Task<ScienceFictions> GetScienceFictionByIdAsync(string id)
    {
        return await _scienceFictionsRepository.GetScienceFictionByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<ScienceFictions>> InsertScienceFictionAsync(ScienceFictions scienceFiction)
    {
        return _scienceFictionsRepository.InsertScienceFictionAsync(scienceFiction);
    }

    public Task<InsertOrUpdateResult<ScienceFictions>> UpdateScienceFictionAsync(ScienceFictions scienceFiction)
    {
        return _scienceFictionsRepository.UpdateScienceFictionAsync(scienceFiction);
    }
}