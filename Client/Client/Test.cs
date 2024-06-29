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
using Client.api;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using DeepL;
using System.Net.NetworkInformation;


namespace Client
{
    public class Translation
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string lang_orig_words { get; set; }
        public string orig_words { get; set; }
        public string lang_trans_words { get; set; }
        public string trans_words { get; set; }
    }

    public partial class Test : Form
    {
        static int id_Butt_Leng = 1;
        static int id_Butt_Leng_YourL = 1;
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();
        // Клас для зберігання історії перекладів
        public class TranslationHistory
        {
            public string OriginalText { get; set; }
            public string TranslatedText { get; set; }
            public string Language { get; set; }
        }

        // Список для зберігання історії перекладів
        private List<TranslationHistory> translationHistories = new List<TranslationHistory>();

        public Test()
        {
            InitializeComponent();
            InitializeListView();
            ShowWords();
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
                    label_test.ForeColor = Color.Black;
                    label_test.Text = ""; // Clear previous text

                    foreach (var translation in translations)
                    {
                        var language_button = new string[] { translation.lang_orig_words, translation.lang_orig_words };
                        AddToTranslationHistory(translation.orig_words, translation.trans_words, language_button);
                    }
                }
                else
                {
                    label_test.ForeColor = Color.Red;
                    label_test.Text = "NULL";
                }

            }
            catch { Console.WriteLine("Error"); }
        }

        private void InitializeListView()
        {
            // Додайте ListView на вашу форму в дизайнері або програмно
            listView1.View = View.Details;
            listView1.Columns.Add("Original Text", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Translated Text", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Language", -2, HorizontalAlignment.Left);
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string url = "http://localhost:3001/api/Add_translate";

                HttpResponseMessage response = await httpSend.PostAdd_translate(url, token, button3.Text, textBox1.Text.ToString(), button2.Text, textBox2.Text.ToString());

                if ((int)response.StatusCode == 200)
                {
                    label_test.ForeColor = Color.Green;
                    label_test.Text = "Успішно додано";
                    ShowWords();
                }
                else
                {
                    label_test.ForeColor = Color.Red;
                    label_test.Text = "NULL";
                }
            }
            catch (Exception ex)
            {
                label_test.Text = "error occurred";
            }
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

        private void button2_Click(object sender, EventArgs e)
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
            button2.Text = _Len[id_Butt_Leng_YourL];
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

        private void Test_Load(object sender, EventArgs e)
        {

        }
    }
}