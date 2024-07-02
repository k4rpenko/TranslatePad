using Client.api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;



using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public partial class Change_Dictionary : Form
    {
        Http_Send httpSend = new Http_Send();

        private class Notes
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string updated_at { get; set; }
        }

        private List<Notes> translations;

        public int NoteId { get; set; }
        private string initialTitle;
        private string initialContent;

        public Change_Dictionary()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed);
            H1.TextChanged += new EventHandler(H1_TextChanged);
            P1.TextChanged += new EventHandler(P1_TextChanged);

            // Initially disable the buttons
            button1.Enabled = false;
            button2.Enabled = false;
            button1.BackColor = SystemColors.GrayText;
            button2.BackColor = SystemColors.GrayText;
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public async void OpenNotes()
        {
            try
            {
                string url = "http://localhost:3001/api/Open_notes";
                HttpResponseMessage response = await httpSend.GetOpenNotes(url, NoteId);
                if ((int)response.StatusCode == 200)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    translations = JsonConvert.DeserializeObject<List<Notes>>(jsonResponse);
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
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void H1_TextChanged(object sender, EventArgs e)
        {
            CheckIfTextChanged();
        }

        private void P1_TextChanged(object sender, EventArgs e)
        {
            CheckIfTextChanged();
        }

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
                button1.BackColor = button2.BackColor = SystemColors.Control;
            }
        }



        public class Http_Send
        {
            private readonly HttpClient _httpClient;

            public Http_Send()
            {
                _httpClient = new HttpClient();
            }

            public async Task<HttpResponseMessage> GetOpenNotes(string url, int noteId)
            {
                url += "?noteId=" + noteId;
                return await _httpClient.GetAsync(url);
            }

            public async Task<HttpResponseMessage> PutAsync(string url, object data)
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                return await _httpClient.PutAsync(url, content);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Notes note = new Notes
                {
                    id = NoteId,
                    user_id = translations[0].user_id,
                    title = H1.Text,
                    content = P1.Text,
                    updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                string url = "http://localhost:3001/api/Update_note";
                HttpResponseMessage response = httpSend.PutAsync(url, note).Result;
                if ((int)response.StatusCode == 200)
                {
                    MessageBox.Show("Changes saved successfully!");
                    initialTitle = H1.Text;
                    initialContent = P1.Text;
                    CheckIfTextChanged();
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            H1.Text = initialTitle;
            P1.Text = initialContent;
            CheckIfTextChanged();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            _tran.Show();
            this.Hide();
        }

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
            CheckIfTextChanged();
        }
    }
}
