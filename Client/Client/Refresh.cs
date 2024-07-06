namespace Client
{
    public class Refresh
    { 
        public void RefreshN()
        {
            Menu _menu = new Menu();
            Dictionary _dictionary = new Dictionary();
            Notes.translations = null;
            _menu.ShowDictionary();
            _dictionary.ShowDictionary();
        }
        
        public void RefreshT()
        {
            Translate _translate = new Translate();
            Menu _menu = new Menu();
            Translation.translations = null;
            _menu.ShowWords();
            _translate.ShowWords();
        }
    }
}