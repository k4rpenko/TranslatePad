namespace Server.Models;

public class TranslateMo
{
    public string Token { get; set; }
    public string lang_orig_words { get; set; }
    public string orig_words { get; set; }
    public string lang_trans_words { get; set; }
    public string trans_words { get; set; }

    public TranslateMo(string Token, string lang_orig_words, string orig_words, string lang_trans_words, string trans_words)
    {
        this.Token = Token;
        this.lang_orig_words = lang_orig_words;
        this.orig_words = orig_words;
        this.lang_trans_words = lang_trans_words;
        this.trans_words = trans_words;
    }
}