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
    public partial class Change_Dictionary : Form
    {
        Refresh _refresh = new Refresh();
        FormProfile FP = new FormProfile(); // Об'єкт для профілю користувача
        Http_Send httpSend = new Http_Send(); // Об'єкт для відправлення HTTP запитів
        private List<NodeId> translations; // Список нотаток
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном
        string token = File.ReadAllText(RefreshFilePath); // Зчитування токену з файлу
        public class NodeId
        {
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }
        public int NoteId { get; set; } // Ідентифікатор нотатки
        private string initialTitle; // Початковий заголовок нотатки
        private string initialContent; // Початковий вміст нотатки

        public Change_Dictionary()
        {
            Console.WriteLine(NoteId);
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
            H1.TextChanged += new EventHandler(H1_TextChanged);
            P1.TextChanged += new EventHandler(P1_TextChanged);
            Show_users();
            // Початково вимикаємо кнопки
            button1.Enabled = false;
            button2.Enabled = false;
            button1.BackColor = SystemColors.GrayText;
            button2.BackColor = SystemColors.GrayText;
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

        // Закриття форми
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Метод для відкриття нотаток
        public async void OpenNotes()
        {
            translations = await httpSend.GetOpenNotes(NoteId);
            if (translations != null && translations.Count > 0)
            {
                H1.Text = translations[0].title;
                P1.Text = translations[0].content;
                initialTitle = translations[0].title;
                initialContent = translations[0].content;
            }
            else
            {
                Console.WriteLine("translations is null or empty");
            }
        }

        //Зміна тексту заголовку
        private void H1_TextChanged(object sender, EventArgs e)
        {
            CheckIfTextChanged();
        }

        // Зміна тексту вмісту
        private void P1_TextChanged(object sender, EventArgs e)
        {
            CheckIfTextChanged();
        }

        // Перевірка, чи змінився текст заголовку або вмісту
        private void CheckIfTextChanged()
        {
            if (P1.Text != initialContent || H1.Text != initialTitle)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button1.BackColor = button2.BackColor = System.Drawing.Color.FromArgb(140, 140, 140);
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button1.BackColor = button2.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            }
        }

        // Натискання на кнопку скасування змін
        private void button1_Click(object sender, EventArgs e)
        {
            H1.Text = initialTitle;
            P1.Text = initialContent;
            CheckIfTextChanged();
        }

        // Натискання на кнопку для переходу до форми перекладу
        private void button5_Click(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            _tran.Show();
            this.Hide();
        }

        // Натискання на кнопку для повернення до меню
        private void button6_Click(object sender, EventArgs e)
        {
            Menu _Menu = new Menu();
            _Menu.StartPosition = FormStartPosition.Manual;
            _Menu.Location = this.Location;
            _Menu.Show();
            this.Hide();
        }

       
        private void P1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        // Обробник події натискання на кнопку збереження змін
        private async void button2_Click(object sender, EventArgs e)
        {
            await httpSend.PostChangeNotes(NoteId, H1.Text.ToString(), P1.Text.ToString());
            initialTitle = H1.Text;
            initialContent = P1.Text;
            CheckIfTextChanged();
            _refresh.RefreshN();
        }

        // Натискання на кнопку для повернення до меню
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку для переходу до форми перекладу
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.Show();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку для виклику форми налаштування профілю
        private void button7_Click(object sender, EventArgs e)
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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
