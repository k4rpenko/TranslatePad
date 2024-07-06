using System.Collections.Generic;

namespace Client
{
    public class Notes
    {
        public static List<Notes> translations;
        
        private Notes() {}
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string updated_at { get; set; }
    }
}