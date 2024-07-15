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
public class user : ControllerBase
{
    public readonly ConnectDB _Connect; 

    public user(ConnectDB Connect) { _Connect = Connect; }

    [HttpPost("Show_users")]
    public async Task<IActionResult> Show_users(OnlyToken _model)
    {
        bool Users = false;
        string result = null;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                var token = Convert.ToInt32(tokenService.DecodeToken(_model.Token));
                if (token != null)
                {
                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.trap_users WHERE id = @Id;", connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", token);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dataTable = new System.Data.DataTable();
                            dataTable.Load(reader);
                            result = JsonConvert.SerializeObject(dataTable);
                            if (result != null)
                            {
                                Users = true;
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

        if (Users)
        {
            return Ok(new { result });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
}