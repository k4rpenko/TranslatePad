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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DeepL;

namespace Client
{
    public partial class Translate : Form
    {
        string apiKey = "7701e92c-c850-490b-ac45-0011d6d74a16:fx";
        Http_Send httpSend = new Http_Send();
        static int id_Butt_Leng = 1;
        static int id_Butt_Leng_YourL = 1;
        public Translate()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
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
                DeepL.Model.TextResult translatedText;

                if (text_for_trans.Text == null)
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

                
            }
            catch (Exception ex) { throw new Exception("Помилка під час ділення на нуль", ex); }
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
    }
}
