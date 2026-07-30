using System.Collections.Generic;
using System.Threading.Tasks;

public class MasterBoardService : IMasterBoardService
{
    private readonly IMasterBoardRepository _masterBoardRepository;

    public MasterBoardService(IMasterBoardRepository masterBoardRepository)
    {
        _masterBoardRepository = masterBoardRepository;
    }

    public static IMasterBoardService Create() => ServiceContainer.GetService<IMasterBoardService>();

    public async Task<List<string>> GetUniqueNameAsync()
    {
        return await _masterBoardRepository.GetUniqueNameAsync();
    }
}