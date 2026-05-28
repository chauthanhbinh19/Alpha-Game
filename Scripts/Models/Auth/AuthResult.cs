public class AuthResult
{
    public bool Success { get; set; }
    public string ErrorField { get; set; }
    public string ErrorMessage { get; set; }
    public User User { get; set; }
}
