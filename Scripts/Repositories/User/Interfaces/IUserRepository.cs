using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<string> RegisterUserAsync(string username, string password);
    Task<User> SignInWithUsernameAndPasswordAsync(string username, string password);
    Task<User> SignInWithoutUsernameAndPasswordAsync(string userId);
    Task<User> GetUserByIdAsync(string Id);
    Task UpdateUserNameAsync(string user_id, string new_name);
    Task UpdateUserPowerAsync(string user_id, double power);
    Task CreateUserCurrencyAsync(string Id);
    Task<bool> CheckNameExistsAsync(string name);
}