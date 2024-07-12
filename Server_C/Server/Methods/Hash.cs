using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Server.Methods;

public class Hash
{
    public string password { get; set; }

    public Hash(string password)
    {
        this.password = password;
    }

    public string HashPassword256()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}