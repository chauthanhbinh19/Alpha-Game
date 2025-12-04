using System.Collections.Generic;
using System.Threading.Tasks;

public class MasterBoardService : IMasterBoardService
{
    private readonly MasterBoardRepository _masterBoardRepository;

    public MasterBoardService(MasterBoardRepository masterBoardRepository)
    {
        _masterBoardRepository = masterBoardRepository;
    }

    public static MasterBoardService Create()
    {
        return new MasterBoardService(new MasterBoardRepository());
    }

    public async Task<List<string>> GetUniqueNameAsync()
    {
        return await _masterBoardRepository.GetUniqueNameAsync();
    }
}