using Client.api;
using Newtonsoft.Json;
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
using static System.Windows.Forms.AxHost;

namespace Client
{
    public partial class Menu : Form
    {
        FormProfile _FP = new FormProfile();
        public List<Notes> translations;

        public class Notes
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();



        public Menu()
        {
            InitializeComponent();
            ShowDictionary();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
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
                    showDictionary_panel();
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

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            _tran.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            _dict.Show();
            this.Hide();
        }

        private void panel3_Click(object sender, PaintEventArgs e)
        {
            if (_FP != null && !_FP.IsDisposed)
            {
                _FP.Focus();
            }
        }



        private void CreatePictureBox(int index)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Image = Image.FromFile("free-icon-font-file.png"); // Замініть шлях на реальний шлях до вашого зображення
            pictureBox.Location = new Point((this.ClientSize.Width - pictureBox.Width) / 2, (this.ClientSize.Height - pictureBox.Height) / 2);
            panel_for_button.Controls.Add(pictureBox);
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Create_Recent_Button(int index, string text)
        {
            Button newButton = new Button();
            newButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            newButton.FlatStyle = FlatStyle.Popup;
            newButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            newButton.ForeColor = Color.FromArgb(140, 140, 140);
            newButton.Margin = new Padding(2);
            newButton.Name = $"dynamicButton{index}";
            newButton.Size = new Size(buttonWidth, buttonHeight);
            newButton.TabIndex = index;
            newButton.Text = text;
            newButton.Click += new EventHandler(DynamicButton_Click);

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
            Button newButton = new Button();
            newButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            newButton.FlatStyle = FlatStyle.Popup;
            newButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            newButton.ForeColor = Color.FromArgb(140, 140, 140);
            newButton.Margin = new Padding(2);
            newButton.Name = $"dynamicButton{index}";
            newButton.Size = new Size(buttonWidth, buttonHeight);
            newButton.Text = text;
            newButton.TabIndex = index;
            newButton.Click += new EventHandler(DynamicButton_Click);

            // Розрахунок позиції нової кнопки
            int x = startX_panel + (buttonCounter_panel * (buttonWidth_panel + spacing_panel));
            int y = startY_panel;

            // Якщо кнопка не вміщується в ширину форми, переносимо на наступний рядок
            if (x + buttonWidth_panel > this.ClientSize.Width)
            {
                buttonCounter_panel = 0;
                startY_panel += buttonHeight_panel + spacing_panel;
                x = startX + (buttonCounter_panel * (buttonWidth_panel + spacing_panel));
                y = startY_panel;
            }

            newButton.Location = new Point(x, y);
            panel_for_button.Controls.Add(newButton);
            buttonCounter_panel++;
        }

        private async void DynamicButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Change_Dictionary _CD = new Change_Dictionary();
            _CD.NoteId = clickedButton.TabIndex;
            _CD.StartPosition = FormStartPosition.Manual;
            _CD.Location = this.Location;
            _CD.OpenNotes();
            _CD.Show();
            this.Hide();

        }

        internal void MenuClosed()
        {
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (_FP == null || _FP.IsDisposed)
            {
                _FP = new FormProfile();
            }

            Point buttonLocationOnScreen = button3.PointToScreen(Point.Empty);
            _FP.StartPosition = FormStartPosition.Manual;
            _FP.Location = new Point(buttonLocationOnScreen.X, buttonLocationOnScreen.Y + button3.Height);
            _FP.TopMost = true;
            _FP.Show();

            _FP.Deactivate += (s, args) => _FP.Close();
        }
    }


}
