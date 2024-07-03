using Client.api;
using Client.pass;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System;

namespace Client
{

    public partial class Authorization : Form
    {
        private static string RefreshFilePath = "user_refresh.txt";
        PassValidation passValidation = new PassValidation();
        EmailValidation emailValidation = new EmailValidation();
        Http_Send httpSend = new Http_Send();
        HttpStatusCode statusCode = HttpStatusCode.NotFound;

        public static void SaveRefreshToken(string refreshToken)
        {
            try
            {
                using (FileStream fs = File.Create(RefreshFilePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(refreshToken);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex) { }
        }

        public static void OpenRefreshToken()
        {
            try
            {
                using (StreamReader sr = File.OpenText("user_refresh.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception ex) { }
        }

        public Authorization()
        {
            InitializeComponent();

            // Підписка на події для чекбоксів
            checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
        }

        #region WorkWithButton
        private async void Sign_in_button_Click_1(object sender, EventArgs e)
        {
            label_in.ForeColor = Color.Red;
            var aEmail = emailValidation.ValidateEmail(Sign_in_email.Text);
            if (!aEmail.IsValid)
            {
                label_in.Text = aEmail.Message;
                return;
            }
            var aPass = passValidation.ValidatePassword(Sign_in_pass.Text);
            if (!aPass.IsValid)
            {
                label_in.Text = aPass.Message;
                return;
            }

            label_in.Text = "";
            Console.WriteLine($"email: {Sign_in_email.Text.ToString()}\nPassword: {Sign_in_pass.Text.ToString()}");
            try
            {
                string url = "https://translate-pad.vercel.app/api/auth/Login";
                HttpResponseMessage response = await httpSend.PostAuth(url, Sign_in_email.Text.ToString(), Sign_in_pass.Text.ToString());
                if (response != null)
                {
                    if ((int)response.StatusCode == 200)
                    {
                        using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                        {
                            var result = await streamReader.ReadToEndAsync();
                            dynamic jsonResponse = JObject.Parse(result);
                            string refreshToken = jsonResponse.refreshToken;
                            SaveRefreshToken(refreshToken);

                            label_in.ForeColor = Color.Green;
                            label_in.Text = "Вхід пройшла успішно";
                            Menu _menu = new Menu();
                            _menu.Show();
                            this.Hide();
                        }
                    }
                    else if ((int)response.StatusCode == 404)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text = "За вказаною адресою електронної пошти немає облікового запису. Ви можете зареєструвати обліковий запис на цю адресу електронної пошти";
                    }
                    else if ((int)response.StatusCode == 401)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text = "Невірні облікові дані";
                    }
                }
                else
                {
                    label_in.ForeColor = Color.Red;
                    label_in.Text = "NULL";
                }
            }
            catch
            {
                label_in.Text = "error occurred";
            }
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            var aEmail = emailValidation.ValidateEmail(Sign_up_email.Text);
            if (!aEmail.IsValid)
            {
                label1.Text = aEmail.Message;
                return;
            }
            if (Sign_up_pass.Text != Sign_up_pass2.Text)
            {
                label1.Text = "Pass is not correct";
                return;
            }
            var aPass = passValidation.ValidatePassword(Sign_up_pass2.Text);
            if (!aPass.IsValid)
            {
                label1.Text = aPass.Message;
                return;
            }

            label1.Text = "";

            try
            {
                string url = "https://translate-pad.vercel.app/api/auth/Regists";
                HttpResponseMessage response = await httpSend.PostAuth(url, Sign_up_email.Text.ToString(), Sign_up_pass.Text.ToString());
                int statusCodeValue = (int)statusCode;

                if ((int)response.StatusCode == 200)
                {
                    using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        var result = await streamReader.ReadToEndAsync();
                        dynamic jsonResponse = JObject.Parse(result);
                        string refreshToken = jsonResponse.refreshToken;
                        SaveRefreshToken(refreshToken);

                        label1.ForeColor = Color.Green;
                        label1.Text = "Вхід пройшла успішно";
                        Menu _menu = new Menu();
                        _menu.Show();
                        this.Hide();
                    }
                }
                else if ((int)response.StatusCode == 404)
                {
                    label1.ForeColor = Color.Red;
                    label1.Text = "Вказана електронна адреса вже використовується, будь ласка, спробуйте ввести іншу або авторизуйтесь.";
                }
                else
                {
                    label1.ForeColor = Color.Red;
                    label1.Text = "Виникла непередбачувана помилка, будь ласка, спробуйте пізніше";
                }
            }
            catch
            {
                label1.Text = "error occurred";
            }
        }
        #endregion

        #region WorkWithPassword
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Sign_up_pass.UseSystemPasswordChar = true;
                Sign_up_pass2.UseSystemPasswordChar = true;
            }
            else
            {
                Sign_up_pass.UseSystemPasswordChar = false;
                Sign_up_pass2.UseSystemPasswordChar = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Sign_in_pass.UseSystemPasswordChar = true;
            }
            else
            {
                Sign_in_pass.UseSystemPasswordChar = false;
            }
        }
        #endregion

        private void ShowError(string message)
        {
            label1.Text = message;
            label1.ForeColor = Color.Red;
            label1.Visible = true;
        }

        private void ShowError2(string message)
        {
            label_in.Text = message;
            label_in.ForeColor = Color.Red;
            label_in.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Уведіть правильну адресу електронної пошти";
            label1.ForeColor = Color.Red;
            label1.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void guna2Button1_Click(object sender, EventArgs e) { }
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void Sign_up_pass_TextChanged_1(object sender, EventArgs e) { }
        private void Sign_up_pass2_TextChanged_1(object sender, EventArgs e) { }

        private void Sign_in_pass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
