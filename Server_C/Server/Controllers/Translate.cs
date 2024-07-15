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
public class Translate : ControllerBase
{
    public readonly ConnectDB _Connect; 

    public Translate(ConnectDB Connect) { _Connect = Connect; }

    [HttpPost("Add_translate")]
    public async Task<IActionResult> Add_translate(TranslateMo _model)
    {
        bool AddTranslate = false;
        try
        {
            _Connect.ConnectAndQuery(connection =>
            {
                var token = Convert.ToInt32(tokenService.DecodeToken(_model.Token));
                if (token != null)
                {
                    using (var cmd = new NpgsqlCommand("INSERT INTO public.trap_translations (user_id, lang_orig_words, orig_words, lang_trans_words, trans_words) VALUES (@User_id, @Lang_orig_words, @Orig_words, @Lang_trans_words, @Trans_words);", connection))
                    {
                        cmd.Parameters.AddWithValue("@User_id", token);
                        cmd.Parameters.AddWithValue("@Lang_orig_words", _model.lang_orig_words);
                        cmd.Parameters.AddWithValue("@Orig_words", _model.orig_words);
                        cmd.Parameters.AddWithValue("@Lang_trans_words", _model.lang_trans_words);
                        cmd.Parameters.AddWithValue("@Trans_words", _model.trans_words);
                        var result = cmd.ExecuteScalar();
                        AddTranslate = true;
                    }
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (AddTranslate)
        {
            return Ok(new { message = "Creat" });
        }
        else
        {
            return Unauthorized(new { message = "Error" });
        }
    }
    
    [HttpPost("Show_translate")]
    public async Task<IActionResult> Show_translate(OnlyToken _model)
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
                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.trap_translations WHERE user_id = @User_id;", connection))
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
}