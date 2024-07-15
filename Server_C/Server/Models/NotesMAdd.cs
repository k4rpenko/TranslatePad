namespace Server.Models;

public class NotesMAdd
{
    public string Token { get; set; }
    public string title { get; set; }
    public string content { get; set; }

    public NotesMAdd(string Token, string title, string content)
    {
        this.Token = Token;
        this.title = title;
        this.content = content;
    }

}