using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMasterBoardService
{
    Task<List<MasterBoard>> GetUserMasterBoardAsync(string user_id, string name);
    Task InsertUserMasterBoardAsync(string user_id, MasterBoard masterBoard);
    Task UpdateUserMasterBoardAsync(string user_id, MasterBoard masterBoard);
}