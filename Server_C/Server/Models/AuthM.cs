namespace Server.Models;

public class AuthM
{
    public string Email { get; set; }
    public string Password { get; set; }

    public AuthM(string email, string password)
    {
        Email = email;
        Password = password;
    }
}