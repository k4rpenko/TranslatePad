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


        private class Notes
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }

        private int buttonCounter = 0;
        private int startX = 44; // Початкова позиція по X
        private int startY = 46; // Початкова позиція по Y
        private int buttonWidth = 85; // Ширина кнопки
        private int buttonHeight = 85; // Висота кнопки
        private int spacing = 10; // Відстань між кнопками
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();



        public Menu()
        {
            InitializeComponent();
            showDictionary();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
        }

        private async void showDictionary()
        {
            try
            {
                string url = "http://localhost:3001/api/Show_Notes";
                HttpResponseMessage response = await httpSend.GetShowNotes(url, token);
                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Notes> translations = JsonConvert.DeserializeObject<List<Notes>>(jsonResponse);
                    int a = translations.Count;
                    Console.WriteLine(translations.Count);
                    for (int i = 0; i < a; i++)
                    {
                        GenerateElements(translations[i].id, translations[i].title.ToString());
                    }
                    this.Refresh();
                }
                else { Console.WriteLine("NULL"); }
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


        private void GenerateElements(int id, string title)
        {
            Console.WriteLine("Start create");
            // Створюємо Button

            //CreatePictureBox(id);
            CreateButton(id, title);
        }


        private void CreatePictureBox(int index)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Image = Image.FromFile("free-icon-font-file.png"); // Замініть шлях на реальний шлях до вашого зображення
            pictureBox.Location = new Point((this.ClientSize.Width - pictureBox.Width) / 2, (this.ClientSize.Height - pictureBox.Height) / 2);
            panel2.Controls.Add(pictureBox);
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}
