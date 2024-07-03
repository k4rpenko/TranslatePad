﻿using Client.api;
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
using static System.Windows.Forms.AxHost;

namespace Client
{
    public partial class Menu : Form
    {
        private int buttonCounter = 0;
        private int startX = 44; // Початкова позиція по X
        private int startY = 50; // Початкова позиція по Y
        private int buttonWidth = 85; // Ширина кнопки
        private int buttonHeight = 85; // Висота кнопки
        private int spacing = 10; // Відстань між кнопками

        private int buttonCounter_panel = 0;
        private int startX_panel = 0; // Початкова позиція по X
        private int startY_panel = 0; // Початкова позиція по Y
        private int buttonWidth_panel = 85; // Ширина кнопки
        private int buttonHeight_panel = 85; // Висота кнопки
        private int spacing_panel = 10; // Відстань між кнопками

        FormProfile _FP = new FormProfile();
        public List<Notes> translations;
        private List<TranslationHistory> translationHistories = new List<TranslationHistory>();
        Http_Send httpSend = new Http_Send();
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);

        public class Notes
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }

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
            InitializeComponent();
            InitializeListView();
            ShowWords();
            ShowDictionary();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private async void ShowDictionary()
        {
            try
            {
                string url = "https://translate-pad.vercel.app/api/Show_Notes";
                HttpResponseMessage response = await httpSend.GetShowNotes(url, token);
                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    translations = JsonConvert.DeserializeObject<List<Notes>>(jsonResponse);
                    int a = translations.Count;

                    if (a >= 5) { a = 5; }
                    for (int i = 0; i < a; i++)
                    {
                        Create_Recent_Button(translations[i].id, translations[i].title.ToString());
                    }
                    this.Refresh();
                    //showDictionary_panel();
                }
                else { Console.WriteLine("NULL"); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private async void showDictionary_panel()
        {
            try
            {
                int a = translations.Count;
                for (int i = 0; i < a; i++)
                {
                    CreateButton(translations[i].id, translations[i].title.ToString());
                }
                this.Refresh();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }





        private void Create_Recent_Button(int index, string text)
        {
            Guna.UI2.WinForms.Guna2Button newButton = new Guna.UI2.WinForms.Guna2Button();
            newButton.Animated = true;
            newButton.BorderRadius = 12;
            newButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            newButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            newButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            newButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            newButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            newButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            newButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            newButton.Location = new System.Drawing.Point(53, 78);
            newButton.Margin = new System.Windows.Forms.Padding(2);
            newButton.Name = $"dynamicButton{index}";
            newButton.Size = new Size(buttonWidth, buttonHeight);
            newButton.TabIndex = index;
            newButton.Text = text;
            //newButton.Click += new EventHandler(DynamicButton_Click);
            // Розрахунок позиції нової кнопки
            int x = startX + (buttonCounter * (buttonWidth + spacing));
            int y = startY;

            // Якщо кнопка не вміщується в ширину форми, переносимо на наступний рядок
            if (x + buttonWidth > this.ClientSize.Width)
            {
                buttonCounter = 0;
                startY += buttonHeight + spacing;
                x = startX + (buttonCounter * (buttonWidth + spacing));
                y = startY;
            }

            newButton.Location = new Point(x, y);
            panel3.Controls.Add(newButton);
            buttonCounter++;
        }


        private void CreateButton(int index, string text)
        {
            Guna.UI2.WinForms.Guna2Button newButton = new Guna.UI2.WinForms.Guna2Button();
            newButton.Animated = true;
            newButton.BorderRadius = 12;
            newButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            newButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            newButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            newButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            newButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            newButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            newButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            newButton.Location = new System.Drawing.Point(53, 78);
            newButton.Margin = new System.Windows.Forms.Padding(2);
            newButton.Name = $"dynamicButton{index}";
            newButton.Size = new Size(buttonWidth, buttonHeight);
            newButton.TabIndex = index;
            //newButton.Click += new EventHandler(DynamicButton_Click);
            // Розрахунок позиції нової кнопки
            int x = startX + (buttonCounter * (buttonWidth + spacing));
            int y = startY;

            // Якщо кнопка не вміщується в ширину форми, переносимо на наступний рядок
            if (x + buttonWidth > this.ClientSize.Width)
            {
                buttonCounter = 0;
                startY += buttonHeight + spacing;
                x = startX + (buttonCounter * (buttonWidth + spacing));
                y = startY;
            }

            newButton.Location = new Point(x, y);
            panel3.Controls.Add(newButton);
            buttonCounter++;
        }

        /*private async void DynamicButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button newButton = new Guna.UI2.WinForms.Guna2Button();
            Change_Dictionary _CD = new Change_Dictionary();
            _CD.NoteId = clickedButton.TabIndex;
            _CD.StartPosition = FormStartPosition.Manual;
            _CD.Location = this.Location;
            _CD.OpenNotes();
            _CD.Show();
            this.Hide();

        }*/
    }
}
