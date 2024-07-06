using System.Collections.Generic;

namespace Client
{
    public class TranslationHistory
    {
        public static List<TranslationHistory> translationHistories = new List<TranslationHistory>();

        public TranslationHistory() {}
        
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
        public string Language { get; set; }
    }
}