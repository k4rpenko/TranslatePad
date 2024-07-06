using Client.api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace Client
{
    public partial class Dictionary : Form
    {
        Refresh _refresh = new Refresh();
        FormProfile FP = new FormProfile(); // Об'єкт для профілю користувача

        private int buttonCounter = 0; // Лічильник кнопок
        private int startX = 7; // Початкова позиція по X
        private int startY = 264; // Початкова позиція по Y
        private int buttonWidth = 183; // Ширина кнопки
        private int buttonHeight = 46; // Висота кнопки
        private int spacing = 10; // Відстань між кнопками
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном
        string token = File.ReadAllText(RefreshFilePath); // Читання токена з файлу
        Http_Send httpSend = new Http_Send(); // Об'єкт для відправлення HTTP запитів
        
        public Dictionary()
        {
            InitializeComponent();
            ShowDictionary(); // Відображення словника при ініціалізації
            Show_users();
        }
        public async void Show_users()
        {
            if(Users.Users_p == null) {Users.Users_p = await httpSend.GetShowUsers(token);}
            if (Users.Users_p != null && Users.Users_p.Count > 0)
            {
                button7.Text = Users.Users_p[0].nick;
                guna2PictureBox1.ImageLocation = Users.Users_p[0].avatar;
            }
        }

        // Метод для відображення словника
        public async void ShowDictionary()
        {
            
            if(Notes.translations == null) { Notes.translations = await httpSend.GetShowNotes(token); }
            if (Notes.translations != null)
            {
                int a = Notes.translations.Count;
                Console.WriteLine(Notes.translations.Count);
                if (a >= 5) { a = 5; }
                for (int i = 0; i < a; i++)
                {
                    CreateButton(Notes.translations[i].id, Notes.translations[i].title.ToString());
                }
                Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Подія закриття форми
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Натискання на кнопку для додавання нової нотатки
        private async void button1_Click(object sender, EventArgs e)
        {
            await httpSend.PostAddNotes(token, H1.Text, P1.Text);
            _refresh.RefreshN();
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

        // Натискання на кнопку для переходу до форми перекладу
        private void button5_Click(object sender, EventArgs e)
        {
            Translate _dict = new Translate();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            _dict.Show();
            this.Hide();
        }

        // Натискання на кнопку для повернення до меню
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

        // Метод для генерації елементів (кнопок)
        private void GenerateElements(int id, string title)
        {
            Console.WriteLine("Start create");
            CreateButton(id, title);
        }

        // Метод для створення нової кнопки
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
            newButton.Text = text;
            newButton.Click += new EventHandler(DynamicButton_Click);

            // Розрахунок позиції нової кнопки
            int x = startX;
            int y = startY + (buttonCounter * (buttonHeight + spacing));
            newButton.Location = new Point(x, y);
            panel2.Controls.Add(newButton);
            buttonCounter++;
        }

        // Натискання на динамічно створену кнопку
        private async void DynamicButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button newButton = sender as Guna.UI2.WinForms.Guna2Button;
            Change_Dictionary _CD = new Change_Dictionary();
            Console.WriteLine(newButton.TabIndex);
            _CD.NoteId = newButton.TabIndex; // Встановлення ідентифікатора нотатки для редагування
            _CD.StartPosition = FormStartPosition.Manual;
            _CD.Location = this.Location;
            _CD.OpenNotes(); // Відкриття нотатки
            _CD.Show();
            this.Hide();
        }

        // Натискання на кнопку для повернення до меню
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку для переходу до форми перекладу
        private void button2_Click_1(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.Show();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку для відкриття профілю користувача
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (FP == null || FP.IsDisposed)
            {
                FP = new FormProfile();
            }

            Point buttonLocationOnScreen = button7.PointToScreen(Point.Empty);
            FP.StartPosition = FormStartPosition.Manual;
            FP.Location = new Point(buttonLocationOnScreen.X, buttonLocationOnScreen.Y + button7.Height);
            FP.TopMost = true;
            FP.Show();

            FP.Deactivate += (s, args) => FP.Close();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void H1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
