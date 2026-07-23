using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMasterBoardService : IUserMasterBoardService
{
    private static UserMasterBoardService _instance;
    private readonly IUserMasterBoardRepository _userMesterBoardRepository;

    public UserMasterBoardService(IUserMasterBoardRepository userMesterBoardRepository)
    {
        _userMesterBoardRepository = userMesterBoardRepository;
    }

    public static UserMasterBoardService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMasterBoardService(new UserMasterBoardRepository());
        }
        return _instance;
    }

    public async Task<List<MasterBoard>> GetUserMasterBoardAsync(string userId, string name)
    {
        return await _userMesterBoardRepository.GetUserMasterBoardAsync(userId, name);
    }

    public async Task InsertUserMasterBoardAsync(string userId, MasterBoard masterBoard)
    {
        await _userMesterBoardRepository.InsertUserMasterBoardAsync(userId, masterBoard);
    }

    public async Task UpdateUserMasterBoardAsync(string userId, MasterBoard masterBoard)
    {
        await _userMesterBoardRepository.UpdateUserMasterBoardAsync(userId, masterBoard);
    }
}