using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMasterBoardService : IUserMasterBoardService
{
    private readonly IUserMasterBoardRepository _userMesterBoardRepository;

    public UserMasterBoardService(IUserMasterBoardRepository userMesterBoardRepository)
    {
        _userMesterBoardRepository = userMesterBoardRepository;
    }

    public static IUserMasterBoardService Create() => ServiceContainer.GetService<IUserMasterBoardService>();

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