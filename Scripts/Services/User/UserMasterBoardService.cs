using System.Collections.Generic;

public class UserMasterBoardService : IUserMasterBoardService
{
    private IUserMasterBoardRepository _userMesterBoardRepository;

    public UserMasterBoardService(IUserMasterBoardRepository userMedalsRepository)
    {
        _userMesterBoardRepository = userMedalsRepository;
    }

    public static UserMasterBoardService Create()
    {
        return new UserMasterBoardService(new UserMasterBoardRepository());
    }

    public List<MasterBoard> GetUserMasterBoard(string user_id, string name)
    {
        return _userMesterBoardRepository.GetUserMasterBoard(user_id, name);
    }

    public void InsertUserMasterBoard(string user_id, MasterBoard masterBoard)
    {
        _userMesterBoardRepository.InsertUserMasterBoard(user_id, masterBoard);
    }

    public void UpdateUserMasterBoard(string user_id, MasterBoard masterBoard)
    {
        _userMesterBoardRepository.UpdateUserMasterBoard(user_id, masterBoard);
    }
}