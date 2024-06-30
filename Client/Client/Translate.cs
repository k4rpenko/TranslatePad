using Client.api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeepL;
using DeepL.Model;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Translate : Form
    {
        string apiKey = "7701e92c-c850-490b-ac45-0011d6d74a16:fx";
        private static string RefreshFilePath = "user_refresh.txt";
        string fileName = "translation_history.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();
        static int id_Butt_Leng = 1;
        static int id_Butt_Leng_YourL = 1;

        public class Translation
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string lang_orig_words { get; set; }
            public string orig_words { get; set; }
            public string lang_trans_words { get; set; }
            public string trans_words { get; set; }
        }

        // Клас для зберігання історії перекладів
        public class TranslationHistory
        {
            public string OriginalText { get; set; }
            public string TranslatedText { get; set; }
            public string Language { get; set; }
        }

        // Список для зберігання історії перекладів
        private List<TranslationHistory> translationHistories = new List<TranslationHistory>();

        public Translate()
        {
            InitializeComponent();
            InitializeListView();
            ShowWords();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);

        }
        private async void ShowWords()
        {
            try
            {
                ClearListViewItems();
                string url = "http://localhost:3001/api/Show_translate";
                HttpResponseMessage response = await httpSend.GetShow_translate(url, token);

                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Translation> translations = JsonConvert.DeserializeObject<List<Translation>>(jsonResponse);

                    foreach (var translation in translations)
                    {
                        var language_button = new string[] { translation.lang_orig_words, translation.lang_trans_words };
                        AddToTranslationHistory(translation.orig_words, translation.trans_words, language_button);
                    }
                }
                else
                {
                    Console.WriteLine("NULL");
                }

            }
            catch { Console.WriteLine("Error"); }
        }

        private void InitializeListView()
        {
            // Додайте ListView на вашу форму в дизайнері або програмно
            listView1.View = View.Details;
            listView1.Columns.Add("Original Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Translated Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Language", -2, HorizontalAlignment.Center);
        }

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

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

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
                    if (id_Butt_Leng == 1)
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            tet_tran.Text = text_for_trans.Text;

                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Ukrainian, LanguageCode.EnglishAmerican); tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Polish, LanguageCode.EnglishAmerican); tet_tran.Text = translatedText.Text;
                        }

                    }
                    else if (id_Butt_Leng == 2)
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.English, LanguageCode.Ukrainian); tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            tet_tran.Text = text_for_trans.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Polish, LanguageCode.Ukrainian); tet_tran.Text = translatedText.Text;
                        }

                    }
                    else if (id_Butt_Leng == 3)
                    {
                        if (id_Butt_Leng_YourL == 1)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.English, LanguageCode.Polish); tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 2)
                        {
                            translatedText = await translator.TranslateTextAsync(text_for_trans.Text, LanguageCode.Ukrainian, LanguageCode.Polish); tet_tran.Text = translatedText.Text;
                        }
                        else if (id_Butt_Leng_YourL == 3)
                        {
                            tet_tran.Text = text_for_trans.Text;
                        }
                    }
                }
                var language_button = new string[] { button3.Text, Your_l.Text };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in button4_Click: {ex.Message}");
            }
        }

        private void AddToTranslationHistory(string originalText, string translatedText, string[] language)
        {
            try
            {
                translationHistories.Add(new TranslationHistory
                {
                    OriginalText = originalText,
                    TranslatedText = translatedText,
                    Language = string.Join("/", language)
                });

                // Додавання запису до listView1
                ListViewItem item = new ListViewItem(originalText);
                item.SubItems.Add(translatedText);
                item.SubItems.Add(string.Join("/", language));
                listView1.Items.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddToTranslationHistory: {ex.Message}");
            }
        }

        private void ClearListViewItems()
        {
            listView1.Items.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                text_for_trans.Text = item.SubItems[0].Text;
                tet_tran.Text = item.SubItems[1].Text;

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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримання шляху до документа
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Створення унікального імені файлу на основі дати та часу
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"TranslationHistory_{timestamp}.txt";
                string filePath = Path.Combine(homePath, fileName);

                // Запис нової історії в новий файл
                using (StreamWriter writer = new StreamWriter(filePath, false)) // false, щоб перезаписати файл
                {
                    foreach (var history in translationHistories)
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


        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string url = "http://localhost:3001/api/Add_translate";
                HttpResponseMessage response = await httpSend.PostAdd_translate(url, token, Your_l.Text, text_for_trans.Text.ToString(), button3.Text, tet_tran.Text.ToString());

                if ((int)response.StatusCode == 200)
                {
                    ShowWords();
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Очищення списку історії перекладів
                translationHistories.Clear();

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



        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }
    }
}
