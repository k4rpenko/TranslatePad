using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using NpgsqlTypes;
using Server.Methods;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Notes : ControllerBase
{
    public readonly ConnectDB _Connect; 

    public Notes(ConnectDB Connect) { _Connect = Connect; }

    [HttpPost("Add_Notesn")]
    public async Task<IActionResult> Add_Notesn(NotesMAdd _model)
    {
        bool AddNote = false;
        string token = null;
        string title = null;
        string content = null;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                var token = Convert.ToInt32(tokenService.DecodeToken(_model.Token));
                if (token != null)
                {
                    using (var cmd = new NpgsqlCommand("INSERT INTO public.trap_notes (user_id, title, content) VALUES (@User_id, @Title, @Content);", connection))
                    {
                        cmd.Parameters.AddWithValue("@User_id", token);
                        cmd.Parameters.AddWithValue("@Title", _model.title);
                        cmd.Parameters.AddWithValue("@Content", _model.content);
                        var result = cmd.ExecuteScalar();
                        AddNote = true;
                    }
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (AddNote)
        {
            return Ok(new { message = "Creat" });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
    
    [HttpPost("Show_Notes")]
    public async Task<IActionResult> Show_Notes(OnlyToken _model)
    {
        bool Show = false;
        string result = null;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                var token = Convert.ToInt32(tokenService.DecodeToken(_model.Token));
                if (token != null)
                {
                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.trap_notes WHERE user_id = @User_id ORDER BY updated_at DESC;", connection))
                    {
                        cmd.Parameters.AddWithValue("@User_id", token);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dataTable = new System.Data.DataTable();
                            dataTable.Load(reader);
                            result = JsonConvert.SerializeObject(dataTable);
                            if (result != null)
                            {
                                Show = true;
                            }
                        }
                    }
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (Show)
        {
            return Ok(new { result });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
    
    [HttpPost("Change_note")]
    public async Task<IActionResult> Change_note(Change_note _model)
    {
        bool Change = false;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                using (var cmd = new NpgsqlCommand("UPDATE public.trap_notes SET title = @Title, content = @Content, updated_at = @Updated_at WHERE id = @Id;", connection))
                {
                    cmd.Parameters.AddWithValue("@Title", _model.title);
                    cmd.Parameters.AddWithValue("@Content", _model.content);
                    cmd.Parameters.AddWithValue("@Updated_at", NpgsqlDbType.Timestamp, DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id", _model.id);
                    var result = cmd.ExecuteNonQuery();
    
                    if (result > 0)
                    {
                        Change = true;
                    }
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (Change)
        {
            return Ok(new { message = "Ok" });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
    
    [HttpPost("Show_one_Notes")]
    public async Task<IActionResult> Show_one_Notes(NotesMShow _model)
    {
        bool Show = false;
        string result = null;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.trap_notes WHERE id = @Id;", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", _model.id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataTable = new System.Data.DataTable();
                        dataTable.Load(reader);
                        result = JsonConvert.SerializeObject(dataTable);
                        if (result != null)
                        {
                            Show = true;
                        }
                    }
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (Show)
        {
            return Ok(new { result });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
}