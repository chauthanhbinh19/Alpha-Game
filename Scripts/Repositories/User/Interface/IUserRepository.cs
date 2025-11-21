using System.Collections.Generic;

public interface IUserRepository
{
    User GetUserByUsername(string username);
    string RegisterUser(string username, string password);
    User SignInWithUsernameAndPassword(string username, string password);
    User SignInWithoutUsernameAndPassword(string userId);
    User GetUserById(string Id);
    void UpdateUserName(string user_id, string new_name);
    void UpdateUserPower(string user_id, double power);
    void createUserCurrency(string id);
    bool CheckNameExists(string name);
}