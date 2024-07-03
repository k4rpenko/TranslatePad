using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormProfile : Form
    {
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном

        public FormProfile()
        {
            InitializeComponent();
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
    }
}
