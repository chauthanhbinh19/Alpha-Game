using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMasterBoardRepository
{
    Task<List<MasterBoard>> GetUserMasterBoardAsync(string userId, string name);
    Task InsertUserMasterBoardAsync(string userId, MasterBoard masterBoard);
    Task UpdateUserMasterBoardAsync(string userId, MasterBoard masterBoard);
}