using System.Collections.Generic;
using System.Threading.Tasks;

public class MasterBoardService : IMasterBoardService
{
    private static MasterBoardService _instance;
    private readonly IMasterBoardRepository _masterBoardRepository;

    public MasterBoardService(IMasterBoardRepository masterBoardRepository)
    {
        _masterBoardRepository = masterBoardRepository;
    }

    public static MasterBoardService Create()
    {
        if (_instance == null)
        {
            _instance = new MasterBoardService(new MasterBoardRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueNameAsync()
    {
        return await _masterBoardRepository.GetUniqueNameAsync();
    }
}