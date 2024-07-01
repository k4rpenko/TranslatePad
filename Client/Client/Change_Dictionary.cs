using Client.api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

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
            H1.TextChanged += new EventHandler(H1_TextChanged);
            P1.TextChanged += new EventHandler(P1_TextChanged);
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Implement logic for saving changes or other functionality
        }
    }
}
