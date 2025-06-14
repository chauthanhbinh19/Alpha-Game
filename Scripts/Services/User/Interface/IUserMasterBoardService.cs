using System.Collections.Generic;

public interface IUserMasterBoardService
{
    List<MasterBoard> GetUserMasterBoard(string user_id, string name);
    void InsertUserMasterBoard(string user_id, MasterBoard masterBoard);
    void UpdateUserMasterBoard(string user_id, MasterBoard masterBoard);
}