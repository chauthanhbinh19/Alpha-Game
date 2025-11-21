using System.Collections.Generic;

public interface IUserService
{ 
    string RegisterUser(string username, string password);
    AuthResult SignInWithUsernameAndPassword(string username, string password);
    AuthResult SignInWithoutUsernameAndPassword(string userId);
    User GetUserById(string Id);
    void UpdateUserName(string user_id, string new_name);
    void UpdateUserPower(string user_id, double power);
    void createUserCurrency(string Id);
    bool CheckNameExists(string name);
}