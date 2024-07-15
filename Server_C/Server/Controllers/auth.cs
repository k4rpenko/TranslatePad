using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Npgsql;
using System.Data;
using Server.Methods;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class auth : ControllerBase
{
    public readonly ConnectDB _Connect; 

    public auth(ConnectDB Connect) { _Connect = Connect; }
    
    [HttpPost("Login")]
    public async Task<IActionResult> LoginPost(AuthM _model)
    {
        Hash _hash = new Hash(_model.Password);
        string password;
        string token = null;
        bool userExists = false;
        bool user = false;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                using (var cmd = new NpgsqlCommand("SELECT password, token_refresh FROM public.trap_users WHERE email = @Email;", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", _model.Email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["password"].ToString();
                            token = reader["token_refresh"].ToString();
                            if (_hash.HashPassword256() == password)
                            {
                                userExists = true;
                            }
                            else
                            {
                                userExists = false;
                            }
                        }
                        else
                        {
                            user = true;
                        }
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
            return Ok(new { token });
        }
        else if (user)
        {
            return Unauthorized(new { message = "Такого користувача не існує" });
        }
        else
        {
            return Unauthorized(new { message = "Неправильний логін або пароль" });
        }
    }
    
    [HttpPost("Regists")]
    public async Task<IActionResult> RegisterPost(AuthM _model)
    {
        string id;
        string avatar = "https://54hmmo3zqtgtsusj.public.blob.vercel-storage.com/avatar/Logo-yEeh50niFEmvdLeI2KrIUGzMc6VuWd-a48mfVnSsnjXMEaIOnYOTWIBFOJiB2.jpg";
        bool userExists = false;
        string token = null;
        Hash _hash = new Hash(_model.Password);
        
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                using (var cmd = new NpgsqlCommand("SELECT email FROM public.trap_users WHERE email = @Email;", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", _model.Email);
                    var result = cmd.ExecuteScalar();
                    if(result == null)
                    {
                        using (var addP = new NpgsqlCommand("INSERT INTO public.trap_users (email, password, avatar) VALUES (@Email, @Password, @Avatar);", connection))
                        {
                            addP.Parameters.AddWithValue("@Email", _model.Email);
                            addP.Parameters.AddWithValue("@Password", _hash.HashPassword256());
                            addP.Parameters.AddWithValue("@Avatar", avatar);
                            addP.ExecuteScalar();
                            using (var Show = new NpgsqlCommand("SELECT id FROM public.trap_users WHERE email = @Email;", connection))
                            {
                                Show.Parameters.AddWithValue("@Email", _model.Email);
                                var result3 = Show.ExecuteScalar();
                                id = Convert.ToString(result3);
                                if (result3 != null)
                                {
                                    using (var addToken = new NpgsqlCommand("UPDATE public.trap_users SET token_refresh = @Token;", connection))
                                    {
                                        token = tokenService.GenerateRefreshToken(id);
                                        addToken.Parameters.AddWithValue("@Token", token);
                                        addToken.ExecuteScalar();
                                        userExists = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        userExists = false;
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
            return Ok(new { token });
        }
        else
        {
            return Unauthorized(new { message = "цей акаунт уже існує" });
        }
    }
}