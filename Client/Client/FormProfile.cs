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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class FormProfile : Form
    {
        private static string RefreshFilePath = "user_refresh.txt";

        public FormProfile()
        {
            InitializeComponent();
        }
        private const int WS_THICKFRAME = 0x40000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |=  WS_THICKFRAME;
                return cp;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "user_refresh.txt";
            try
            {
                if (File.Exists(path))
                {
                    Menu _menu = new Menu();
                    _menu.MenuClosed();
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при видаленні файлу: {ex.Message}");
            }
        }
    }
 }
