using Client.api;
using Client.Properties;
using DeepL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Dictionary : Form
    {
        private class Notes
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }

        private int buttonCounter = 0;
        private int startX = 7; // Початкова позиція по X
        private int startY = 264; // Початкова позиція по Y
        private int buttonWidth = 183; // Ширина кнопки
        private int buttonHeight = 46; // Висота кнопки
        private int spacing = 10; // Відстань між кнопками
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();

        public Dictionary()
        {
            InitializeComponent();
            ShowDictionary();
        }

        private void CreateButton()
        {
            for (int i = 0; i < 2; i++)
            {
                GenerateElements(i, "one");
            }
        }


        private async void ShowDictionary()
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
                    if(a >=5) { a = 5; }
                    for (int i = 0; i < a; i++)
                    {
                        GenerateElements(translations[i].id, translations[i].title.ToString());
                    }
                    this.Refresh();
                }
                else { Console.WriteLine("NULL"); }
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://localhost:3001/api/Add_Notesn";
                HttpResponseMessage response = await httpSend.PostAddDictionary(url, token, H1.Text.ToString(), P1.Text.ToString());
                if ((int)response.StatusCode == 200) { Console.WriteLine("Creat note"); }
                else { Console.WriteLine("NULL"); }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Нотатка1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Translate _dict = new Translate();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            _dict.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            _menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }










        private void ClearElements() 
        {
            foreach (var control in this.Controls.OfType<Control>().ToArray())
            {
                if (control.Name.StartsWith("dynamic"))
                {
                    this.Controls.Remove(control);
                }
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
            int x = startX;
            int y = startY + (buttonCounter * (buttonHeight + spacing));

            newButton.Location = new Point(x, y);
            panel2.Controls.Add(newButton);
            buttonCounter++;
        }
    }
}
