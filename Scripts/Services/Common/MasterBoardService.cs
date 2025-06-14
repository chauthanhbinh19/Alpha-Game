using System.Collections.Generic;

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

    public List<string> GetUniqueName()
    {
        return _masterBoardRepository.GetUniqueName();
    }
}