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
}