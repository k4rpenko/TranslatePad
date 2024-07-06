using Client.api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class Menu : Form
    {
        // Параметри для створення кнопок
        private int buttonCounter = 0;
        private int startX = 44; // Початкова позиція по X
        private int startY = 50; // Початкова позиція по Y
        private int buttonWidth = 85; // Ширина кнопки
        private int buttonHeight = 85; // Висота кнопки
        private int spacing = 10; // Відстань між кнопками

        // Параметри для створення кнопок у панелі
        private int buttonCounter_panel = 0;
        private int startX_panel = 0; // Початкова позиція по X
        private int startY_panel = 0; // Початкова позиція по Y
        private int buttonWidth_panel = 85; // Ширина кнопки
        private int buttonHeight_panel = 85; // Висота кнопки
        private int spacing_panel = 10; // Відстань між кнопками

        FormProfile _FP = new FormProfile(); // Стоврення форми для налаштувань профіля
        Http_Send httpSend = new Http_Send(); // Об'єкт для надсилання HTTP запитів
        private static string RefreshFilePath = "user_refresh.txt"; // Шлях до файлу з токеном
        string token = File.ReadAllText(RefreshFilePath); // Зчитування токену з файлу

        // Ініціалізація ListView для відображення історії перекладів
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Original Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Translated Text", 280, HorizontalAlignment.Center);
            listView1.Columns.Add("Language", -2, HorizontalAlignment.Center);
        }


        public Menu()
        {
            InitializeComponent();
            InitializeListView();
            ShowWords(); // Відображення перекладів
            ShowDictionary(); // Відображення словника
            Show_users();
            this.FormClosed += new FormClosedEventHandler(Menu_FormClosed); // Обробник закриття форми
        }

        // Закриття форми
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public async void Show_users()
        {
            if(Users.Users_p == null) {Users.Users_p = await httpSend.GetShowUsers(token);}
            if (Users.Users_p != null && Users.Users_p.Count > 0)
            {
                button6.Text = Users.Users_p[0].nick;
                pictureBox1.ImageLocation = Users.Users_p[0].avatar;
            }
        }


        // Метод для відображення словника
        public async void ShowDictionary()
        {
            if( Notes.translations == null) {Notes.translations = await httpSend.GetShowNotes(token);}
            if (Notes.translations != null)
            {
                int a = Notes.translations.Count;
                if (a >= 5) { a = 5; } 
                for (int i = 0; i < a; i++)
                {
                    Create_Recent_Button(Notes.translations[i].id, Notes.translations[i].title.ToString());
                }
                Refresh();
                showDictionary_panel();
            }
        }

        // Метод для відображення словника у панелі
        private void showDictionary_panel()
        {
            if (Notes.translations != null)
            {
                int a = Notes.translations.Count;
                for (int i = 0; i < a; i++) { CreateButton(Notes.translations[i].id, Notes.translations[i].title.ToString()); }
                Refresh();
            }
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

        //  Натискання на кнопку, яка відкриває форму перекладу
        private void button1_Click_1(object sender, EventArgs e)
        {
            Translate _tran = new Translate();
            _tran.Show();
            _tran.StartPosition = FormStartPosition.Manual;
            _tran.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку, яка відкриває форму словника
        private void button2_Click_1(object sender, EventArgs e)
        {
            Dictionary _dict = new Dictionary();
            _dict.Show();
            _dict.StartPosition = FormStartPosition.Manual;
            _dict.Location = this.Location;
            this.Hide();
        }

        // Натискання на кнопку, яка відкриває форму профілю
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (_FP == null || _FP.IsDisposed)
            {
                _FP = new FormProfile();
            }

            Point buttonLocationOnScreen = button6.PointToScreen(Point.Empty);
            _FP.StartPosition = FormStartPosition.Manual;
            _FP.Location = new Point(buttonLocationOnScreen.X, buttonLocationOnScreen.Y + button6.Height);
            _FP.TopMost = true;
            _FP.Show();

            _FP.Deactivate += (s, args) => _FP.Close();
        }

        // Метод для закриття меню
        internal void MenuClosed()
        {
            Application.Exit();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Метод для додавання запису до історії перекладів
        private void AddToTranslationHistory(string originalText, string translatedText, string[] language)
        {
            TranslationHistory.translationHistories.Add(new TranslationHistory
            {
                OriginalText = originalText,
                TranslatedText = translatedText,
                Language = string.Join("/", language)
            });

            // Додавання запису до listView1
            ListViewItem item = new ListViewItem(originalText);
            item.SubItems.Add(translatedText);
            item.SubItems.Add(string.Join("/", language));
            listView1.Items.Add(item);
        }

        // Метод для відображення перекладів
        public async void ShowWords()
        {
            ClearListViewItems();
            if(Translation.translations == null) { Translation.translations = await httpSend.GetShow_translate(token);}
            
            try
            {
                if (Translation.translations != null && Translation.translations.Count > 0)
                {
                    for (int i = 0; i < 5 && i < Translation.translations.Count; i++)
                    {
                        var translation = Translation.translations[i];
                        var language_button = new[] { translation.lang_orig_words, translation.lang_trans_words };
                        AddToTranslationHistory(translation.orig_words, translation.trans_words, language_button);
                    }
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Обробка винятку, можливо, виведення повідомлення або журналювання помилки
                Console.WriteLine($"Помилка доступу до елементу масиву: {ex.Message}");
            }

        }

        // Метод для очищення елементів ListView
        private void ClearListViewItems()
        {
            listView1.Items.Clear(); 
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        // Метод для створення кнопки з нещодавніми нотатками
        private void Create_Recent_Button(int index, string text)
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
            int x = startX + (buttonCounter * (buttonWidth + spacing));
            int y = startY;

            // Якщо кнопка не вміщується в ширину форми, переносимо на наступний рядок
            if (x + buttonWidth > this.ClientSize.Width)
            {
                buttonCounter = 0;
                startY += buttonHeight + spacing;
                x = startX + (buttonCounter * (buttonWidth + spacing));
                y = startY;
            }

            newButton.Location = new Point(x, y);
            panel3.Controls.Add(newButton);
            buttonCounter++;
        }

        // Метод для створення кнопки у панелі
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
            newButton.Text = text;
            newButton.TabIndex = index;
            newButton.Click += new EventHandler(DynamicButton_Click);

            // Розрахунок позиції нової кнопки
            int x = startX_panel + (buttonCounter_panel * (buttonWidth_panel + spacing_panel));
            int y = startY_panel;

            // Якщо кнопка не вміщується в ширину форми, переносимо на наступний рядок
            if (x + buttonWidth_panel > this.ClientSize.Width)
            {
                buttonCounter_panel = 0;
                startY_panel += buttonHeight_panel + spacing_panel;
                x = startX_panel + (buttonCounter_panel * (buttonWidth_panel + spacing_panel));
                y = startY_panel;
            }

            newButton.Location = new Point(x, y);
            panel_for_button.Controls.Add(newButton);
            buttonCounter_panel++;
        }

        // Натискання на динамічну кнопку
        private async void DynamicButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button newButton = sender as Guna.UI2.WinForms.Guna2Button;
            Change_Dictionary _CD = new Change_Dictionary();
            Console.WriteLine(newButton.TabIndex);
            _CD.NoteId = newButton.TabIndex;
            _CD.StartPosition = FormStartPosition.Manual;
            _CD.Location = this.Location;
            _CD.OpenNotes();
            _CD.Show();
            this.Hide();
        }
    }
}
