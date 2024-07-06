using Client.api;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using DeepL;
using DeepL.Model;
using Newtonsoft.Json;

namespace Client
{
    public partial class Translate : Form
    {
        FormProfile FP = new FormProfile(); // Виклик форми профілю
        string apiKey = "7701e92c-c850-490b-ac45-0011d6d74a16:fx"; // API ключ для DeepL
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном
        string fileName = "translation_history.txt"; // Назва файлу з історією перекладів
        string token = File.ReadAllText(RefreshFilePath); // Зчитування токену з файлу
        Http_Send httpSend = new Http_Send(); // Екземпляр класу для HTTP запитів
        static int id_Butt_Leng = 1; // Ідентифікатор для кнопки вибору мови перекладу
        static int id_Butt_Leng_YourL = 1; // Ідентифікатор для кнопки вибору мови оригіналу
        

        public Translate()
        {
            InitializeComponent();
            InitializeListView(); // Ініціалізація ListView для відображення перекладів
            ShowWords(); // Відображення перекладів
            Show_users();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed); // Додавання обробника події закриття форми
        }
        
        public async void Show_users()
        {
            if(Users.Users_p == null) {Users.Users_p = await httpSend.GetShowUsers(token);}
            if (Users.Users_p != null && Users.Users_p.Count > 0)
            {
                button6.Text = Users.Users_p[0].nick;
                pictureBox1.ImageLocation = Users.Users_p[0].avatar;
            }
        }

        // Метод для відображення перекладів
        private async void ShowWords()
        {
            ClearListViewItems();
            if(Translation.translations == null) { Translation.translations = await httpSend.GetShow_translate(token);}

            if (Translation.translations != null)
            {
                foreach (var translation in Translation.translations)
                {
                    var language_button = new string[] { translation.lang_orig_words, translation.lang_trans_words };
                    AddToTranslationHistory(translation.orig_words, translation.trans_words, language_button); // Додавання до історії перекладів
                }
            }

        }

        // Метод для ініціалізації ListView
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Original Text", 280, HorizontalAlignment.Center); // Додавання колонок до ListView
            listView1.Columns.Add("Translated Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Language", -2, HorizontalAlignment.Center);
        }

        // Закриття форми
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           
        }

        // Натискання на кнопку "Словник"
        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку "Меню"
        private void button7_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку зміни мови перекладу
        private void button3_Click(object sender, EventArgs e)
        {
            id_Butt_Leng++;
            Dictionary<int, string> _Len = new Dictionary<int, string>();
            _Len.Add(1, "EN");
            _Len.Add(2, "UA");
            _Len.Add(3, "PL");
            if (id_Butt_Leng > 3)
            {
                id_Butt_Leng = 1;
            }
            button3.Text = _Len[id_Butt_Leng];
        }

        // Натискання на кнопку перекладу тексту
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var translator = new Translator(apiKey);
                TextResult translatedText;

                if (string.IsNullOrEmpty(text_for_trans.Text))
                {
                    tet_tran.Text = "";
                }
                else
                {
                    if (id_Butt_Leng == 1) // Переклад з англійської мови
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            tet_tran.Text = text_for_trans.Text;
                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Ukrainian, LanguageCode.EnglishAmerican);
                            tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Polish, LanguageCode.EnglishAmerican);
                            tet_tran.Text = translatedText.Text;
                        }
                    }
                    else if (id_Butt_Leng == 2) // Переклад з української мови
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.English, LanguageCode.Ukrainian);
                            tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            tet_tran.Text = text_for_trans.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Polish, LanguageCode.Ukrainian);
                            tet_tran.Text = translatedText.Text;
                        }
                    }
                    else if (id_Butt_Leng == 3) // Переклад з польської мови
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.English, LanguageCode.Polish);
                            tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Ukrainian, LanguageCode.Polish);
                            tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            tet_tran.Text = text_for_trans.Text;
                        }
                    }
                }
                var language_button = new string[] { button3.Text, Your_l.Text }; // Масив з вибраними мовами
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in button4_Click: {ex.Message}");
            }
        }

        // Метод для додавання перекладу до історії перекладів
        private void AddToTranslationHistory(string originalText, string translatedText, string[] language)
        {
            TranslationHistory.translationHistories.Add(new TranslationHistory
            {
                OriginalText = originalText,
                TranslatedText = translatedText,
                Language = string.Join("/", language)
            });

            // Додавання запису до ListView
            ListViewItem item = new ListViewItem(originalText);
            item.SubItems.Add(translatedText);
            item.SubItems.Add(string.Join("/", language));
            listView1.Items.Add(item);
        }

        // Метод для очищення елементів ListView
        private void ClearListViewItems()
        {
            listView1.Items.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        // Натискання на кнопку зміни мови оригіналу
        private void Your_l_Click(object sender, EventArgs e)
        {
            id_Butt_Leng_YourL++;
            Dictionary<int, string> _Len = new Dictionary<int, string>();
            _Len.Add(1, "EN");
            _Len.Add(2, "UA");
            _Len.Add(3, "PL");
            if (id_Butt_Leng_YourL > 3)
            {
                id_Butt_Leng_YourL = 1;
            }
            Your_l.Text = _Len[id_Butt_Leng_YourL];
        }

        //  Вибір елемента в ListView
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                text_for_trans.Text = item.SubItems[0].Text; // Заповнення тексту для перекладу
                tet_tran.Text = item.SubItems[1].Text; // Заповнення перекладеного тексту

                string language = item.SubItems[2].Text;
                switch (language)
                {
                    case "EN":
                        id_Butt_Leng = 1;
                        button3.Text = "EN";
                        break;
                    case "UA":
                        id_Butt_Leng = 2;
                        button3.Text = "UA";
                        break;
                    case "PL":
                        id_Butt_Leng = 3;
                        button3.Text = "PL";
                        break;
                }
            }
        }

        private void text_for_trans_TextChanged(object sender, EventArgs e)
        {

        }

        // Натискання на кнопку для збереження історії перекладів у файл
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"TranslationHistory_{timestamp}.txt";
                string filePath = Path.Combine(homePath, fileName);

                // Запис нової історії в новий файл
                using (StreamWriter writer = new StreamWriter(filePath, false)) 
                {
                    foreach (var history in TranslationHistory.translationHistories)
                    {
                        writer.WriteLine($"Original Text: {history.OriginalText}");
                        writer.WriteLine($"Translated Text: {history.TranslatedText}");
                        writer.WriteLine($"Language: {history.Language}");
                        writer.WriteLine();
                    }
                }

                MessageBox.Show($"Translation history saved to {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in button5_Click: {ex.Message}");
            }
        }

        // Натискання на кнопку додавання нового перекладу
        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string url = "https://translate-pad.vercel.app/api/Add_translate"; // URL для додавання нового перекладу
                HttpResponseMessage response = await httpSend.PostAdd_translate(url, token, Your_l.Text, text_for_trans.Text.ToString(), button3.Text, tet_tran.Text.ToString());

                if ((int)response.StatusCode == 200)
                {
                    ShowWords(); // Оновлення відображення перекладів
                }
                else
                {
                    Console.WriteLine("NULL");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occurred");
            }
        }

        // Натискання на кнопку очищення історії перекладів
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Очищення списку історії перекладів
                TranslationHistory.translationHistories.Clear();

                // Очищення ListView
                ClearListViewItems();

                // Очищення файлу історії перекладів
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(homePath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                MessageBox.Show("Translation history cleared.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in button2_Click_1: {ex.Message}");
            }
        }

        // Натискання на кнопку для переходу до головного меню
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show(); 
            _menu.StartPosition = FormStartPosition.Manual; 
            _menu.Location = this.Location; // Встановлює таке ж місце для форми Menu як і для цієї форми
            this.Hide(); // Ховає поточну форму
        }

        // Натискання на кнопку для переходу до словника
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show(); // Показує форму Dictionary
            _dict.StartPosition = FormStartPosition.Manual; 
            _dict.Location = this.Location; // Встановлює таке ж місце для форми Dictionary як і для цієї форми
            this.Hide(); // Ховає поточну форму
        }

        // Натискання на кнопку для відкриття профілю користувача
        private void button6_Click_1(object sender, EventArgs e)
        {
            if (FP == null || FP.IsDisposed)
            {
                FP = new FormProfile();
            }
            
            Point buttonLocationOnScreen = button6.PointToScreen(Point.Empty);
            
            FP.StartPosition = FormStartPosition.Manual;
            FP.Location = new Point(buttonLocationOnScreen.X, buttonLocationOnScreen.Y + button6.Height);
            FP.TopMost = true; 
            FP.Show(); 
            
            FP.Deactivate += (s, args) => FP.Close();
        }

    }
}
