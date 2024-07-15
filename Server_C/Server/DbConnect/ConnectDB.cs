using Microsoft.EntityFrameworkCore;
using Npgsql;

public class ConnectDB : DbContext
{
    private readonly IConfiguration _configuration;

    public ConnectDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConnectAndQuery(Action<NpgsqlConnection> queryAction)
    {
        try
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                queryAction(connection);
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}