using System.Collections.Generic;

namespace Client
{
    public class Translation
    {
        public static List<Translation> translations;
        
        Translation() {}
        
        public int id { get; set; }
        public int user_id { get; set; }
        public string lang_orig_words { get; set; }
        public string orig_words { get; set; }
        public string lang_trans_words { get; set; }
        public string trans_words { get; set; }
    }
}