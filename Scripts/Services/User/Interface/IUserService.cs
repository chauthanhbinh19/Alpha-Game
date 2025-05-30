using System.Collections.Generic;

public interface IUserService
{ 
    string RegisterUser(string username, string password);
    User SignInUser(string username, string password);
    User GetUserById(string Id);
    void UpdateUserName(string user_id, string new_name);
    void createUserCurrency(string Id);
}