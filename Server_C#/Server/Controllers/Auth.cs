using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Npgsql;
using System.Data;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Auth : ControllerBase
{
    public readonly ConnectDB _Connect;

    public Auth(ConnectDB Connect) { _Connect = Connect; }
    
    [HttpPost("Login")]
    public async Task<IActionResult> LoginPost(AuthM _model)
    {
        bool userExists = false;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM public.trap_users WHERE email = @Email;", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", _model.Email);
                    var result = cmd.ExecuteScalar();
                    Console.WriteLine($"Query result: {result}");
                    if(result != null)
                    {
                        userExists = (long)result > 0;
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }

        if (userExists)
        {
            return Ok(new { message = "Успішний вхід" });
        }
        else
        {
            return Unauthorized(new { message = "Неправильний логін або пароль" });
        }
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterPost(AuthM _model)
    {
        
        return Ok();
    }
}