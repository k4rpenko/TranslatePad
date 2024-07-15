namespace Server.Models;

public class Change_note
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }

    public Change_note(int id, string title, string content)
    {
        this.id = id;
        this.title = title;
        this.content = content;
    }
}