namespace Server.Models;

public class OnlyToken
{
    public string Token { get; set; }
    
    public OnlyToken(string Token)
    {
        this.Token = Token;
    }
}