using Client.api;
using Newtonsoft.Json.Linq;
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

namespace Client
{
    public partial class FormProfile : Form
    {
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном
        string token = File.ReadAllText(RefreshFilePath); // Зчитування токену з файлу
        public List<Users> Users_p;
        Http_Send httpSend = new Http_Send();

        public class Users
        {
            public int id { get; set; }
            public string nick { get; set; }
            public string email { get; set; }
            public string avatar { get; set; }
        }


        public FormProfile()
        {
            Show_users();
            InitializeComponent();
        }

        async void Show_users() 
        {
            try
            {
                string url = "https://translate-pad.vercel.app/api/Show_users";
                HttpResponseMessage response = await httpSend.GetShowNotes(url, token);
                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Users_p = JsonConvert.DeserializeObject<List<Users>>(jsonResponse);
                    label2.Text = Users_p[0].nick;
                    label1.Text = Users_p[0].email; 
                    pictureBox1.ImageLocation = Users_p[0].avatar;
                }
                else { Console.WriteLine("NULL"); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private const int WS_THICKFRAME = 0x40000; // Константа для додавання рамки форми

        // Перевизначення CreateParams для додавання товстої рамки до форми
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_THICKFRAME; // Додавання стилю товстої рамки
                return cp;
            }
        }

        // Обробник події натискання на кнопку
        private void button1_Click(object sender, EventArgs e)
        {
            string path = "user_refresh.txt"; // Шлях до файлу з токеном
            try
            {
                if (File.Exists(path)) // Перевірка наявності файлу
                {
                    Menu _menu = new Menu();
                    _menu.MenuClosed(); // Виклик методу закриття меню
                    File.Delete(path); // Видалення файлу
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при видаленні файлу: {ex.Message}"); // Виведення повідомлення про помилку
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void FormProfile_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
