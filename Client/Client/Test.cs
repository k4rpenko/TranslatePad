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
using Client.api;


namespace Client
{
    public partial class Test : Form
    {
        static int id_Butt_Leng = 1;
        static int id_Butt_Leng_YourL = 1;
        private static string RefreshFilePath = "user_refresh.txt";
        Http_Send httpSend = new Http_Send();

        public Test()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string token = File.ReadAllText(RefreshFilePath);
            try
            {
                string url = "http://localhost:3001/api/Add_translate";
                Console.WriteLine($"url: {url}\ntoken: {token}\nOr_le: {button3.Text}\nOr_Set:{textBox1.Text.ToString()}\nTr_le:{button2.Text}\nTr_set:{textBox2.Text.ToString()}");
                
                HttpResponseMessage response = await httpSend.PostAdd_translate(url, token, button3.Text, textBox1.Text.ToString(), button2.Text, textBox2.Text.ToString());
                Console.WriteLine((int)response.StatusCode);

                if ((int)response.StatusCode == 200)
                {
                    label_test.ForeColor = Color.Green;
                    label_test.Text = "Успішно додано";
                }
                else
                {
                    label_test.ForeColor = Color.Red;
                    label_test.Text = "NULL";
                }
                }
            catch (Exception ex)
            {
                label_test.Text = "error occurred";
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            id_Butt_Leng_YourL++;
            Dictionary<int, string> _Len = new Dictionary<int, string>();
            _Len.Add(1, "EN");
            _Len.Add(2, "UA");
            _Len.Add(3, "PL");
            if (id_Butt_Leng_YourL > 3)
            {
                id_Butt_Leng_YourL = 1;
            }
            button2.Text = _Len[id_Butt_Leng_YourL];
        }
    }
}
