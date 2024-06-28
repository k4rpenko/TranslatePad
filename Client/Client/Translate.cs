using Client.api;
using Newtonsoft.Json.Linq;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Translate : Form
    {
        Http_Send httpSend = new Http_Send();
        static int id_Butt_Leng = 1;
        public Translate()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Menu _menu = new Menu();
            _menu.Show();
            _menu.StartPosition = FormStartPosition.Manual;
            _menu.Location = this.Location;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id_Butt_Leng++;
            Dictionary<int, string> _Len = new Dictionary<int, string>();
            _Len.Add(1, "EN");
            _Len.Add(2, "UA");
            _Len.Add(3, "PL");
            if (id_Butt_Leng > 3)
            {
                id_Butt_Leng = 1;
            }
            button3.Text = _Len[id_Butt_Leng];
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage response = await httpSend.PostTranslate(text_for_trans.Text.ToString(), button3.Text);
                if ((int)response.StatusCode == 200)
                {
                    using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        var result = await streamReader.ReadToEndAsync();
                        dynamic jsonResponse = JObject.Parse(result);
                        string JsonTrans = jsonResponse.refreshToken;

                        MessageBox.Show(JsonTrans, "one");
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Помилка під час ділення на нуль", ex); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
