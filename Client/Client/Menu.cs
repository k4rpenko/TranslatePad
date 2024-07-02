using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Menu : Form
    {

        FormProfile FP = new FormProfile();
        public Menu()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
        }
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
    }
}
