using System.Collections.Generic;

namespace Client
{
    
    
    public class Users
    {
        public static List<Users> Users_p;
        private Users() { }       
        public int id { get; set; }
        public string nick { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
    }
}