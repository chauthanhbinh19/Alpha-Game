using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMasterBoardService : IUserMasterBoardService
{
     private static UserMasterBoardService _instance;
    private IUserMasterBoardRepository _userMesterBoardRepository;

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

    public async Task<List<MasterBoard>> GetUserMasterBoardAsync(string user_id, string name)
    {
        return await _userMesterBoardRepository.GetUserMasterBoardAsync(user_id, name);
    }

    public async Task InsertUserMasterBoardAsync(string user_id, MasterBoard masterBoard)
    {
        await _userMesterBoardRepository.InsertUserMasterBoardAsync(user_id, masterBoard);
    }

    public async Task UpdateUserMasterBoardAsync(string user_id, MasterBoard masterBoard)
    {
        await _userMesterBoardRepository.UpdateUserMasterBoardAsync(user_id, masterBoard);
    }
}