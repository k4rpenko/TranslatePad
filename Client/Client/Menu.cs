using Client.api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Web.Security;

namespace Client
{

    public partial class Menu : Form
    {

        private List<TranslationHistory> translationHistories = new List<TranslationHistory>();
        Http_Send httpSend = new Http_Send();
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);





        public class Translation
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string lang_orig_words { get; set; }
            public string orig_words { get; set; }
            public string lang_trans_words { get; set; }
            public string trans_words { get; set; }
        }

        public class TranslationHistory
        {
            public string OriginalText { get; set; }
            public string TranslatedText { get; set; }
            public string Language { get; set; }
        }

        private void InitializeListView()
        {
            
            listView1.View = View.Details;
            listView1.Columns.Add("Original Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Translated Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Language", -2, HorizontalAlignment.Center);
        }


 


        FormProfile FP = new FormProfile();
        public Menu()
        {
            InitializeListView();
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
        }
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.Show();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
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

        internal void MenuClosed()
        {
            Application.Exit();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private async void ShowWords()
        {
            try
            {
                ClearListViewItems();
                string url = "https://translate-pad.vercel.app/api/Show_translate";
                HttpResponseMessage response = await httpSend.GetShow_translate(url, token);

                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Translation> translations = JsonConvert.DeserializeObject<List<Translation>>(jsonResponse);
                    for (int i = 0; i < 5; i++)
                    {
                        var translation = translations[i];
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
        private void ClearListViewItems() { listView1.Items.Clear(); }



    }
}
