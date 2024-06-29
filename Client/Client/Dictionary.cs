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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
        private static string RefreshFilePath = "user_refresh.txt";
        string token = File.ReadAllText(RefreshFilePath);
        Http_Send httpSend = new Http_Send();

        public Dictionary()
        {
            InitializeComponent();
            ShowDictionary();
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
                    Console.WriteLine(translations.Count);
                    ClearElements();
                    for (int i = 0; i < translations.Count; i++)
                    {
                        GenerateElements(translations[i].id, translations[i].title);
                        if(i == 5) { return; }
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

            CreateButton(id, title);
            CreatePictureBox(id);
        }


        private void CreatePictureBox(int index)
        {
            Console.WriteLine("Start Picture");
            PictureBox pictureBox = new PictureBox();
            Controls.Add(pictureBox);
            pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("free-icon-font-file.png")));
            pictureBox.Location = new System.Drawing.Point(11, 259 + (index * 70));
            pictureBox.Margin = new Padding(2);
            pictureBox.Name = $"dynamicPictureBox{index}";
            pictureBox.Size = new System.Drawing.Size(20, 26);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 12;
            pictureBox.TabStop = false;
        }

        private void CreateButton(int index, string text)
        {
            Console.WriteLine("Start button");
            Button button = new Button();
            Controls.Add(button);
            button.BackColor = Color.FromArgb(37, 37, 37);
            button.FlatStyle = FlatStyle.Popup;
            button.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.ForeColor = Color.FromArgb(140, 140, 140);
            button.Location = new System.Drawing.Point(7, 252 + (index * 70));
            button.Margin = new Padding(2);
            button.Name = $"dynamicButton{index}";
            button.Size = new System.Drawing.Size(183, 46);
            button.TabIndex = 11;
            button.Text = text;
            button.UseVisualStyleBackColor = false;

        }
    }
}
