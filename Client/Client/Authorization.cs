using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.pass;
using Client.api;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;


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
            SetEmailPlaceholder(Sign_up_email, "Email");
            SetEmailPlaceholder(Sign_in_email, "Email");
            SetPasswordPlaceholder(Sign_up_pass, "Password");
            SetPasswordPlaceholder(Sign_up_pass2, "Repeat Password");
            SetPasswordPlaceholder(Sign_in_pass, "Password");

            Sign_up_email.Enter += RemovePlaceholder;
            Sign_up_email.Leave += (sender, e) => SetEmailPlaceholder(Sign_up_email, "Email");
            Sign_in_email.Enter += RemovePlaceholder;
            Sign_in_email.Leave += (sender, e) => SetEmailPlaceholder(Sign_in_email, "Email");
            Sign_up_pass.Enter += RemovePlaceholder;
            Sign_up_pass.Leave += (sender, e) => SetPasswordPlaceholder(Sign_up_pass, "Password");
            Sign_up_pass2.Enter += RemovePlaceholder;
            Sign_up_pass2.Leave += (sender, e) => SetPasswordPlaceholder(Sign_up_pass2, "Repeat Password");
            Sign_in_pass.Enter += RemovePlaceholder;
            Sign_in_pass.Leave += (sender, e) => SetPasswordPlaceholder(Sign_in_pass, "Password");

        }

        #region WorkWithButton
        private async void Sign_in_button_Click(object sender, EventArgs e)
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
                string url = "http://localhost:3001/api/auth/Login";
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
                            //TestForm form1 = new TestForm();
                            //form1.Show();
                            //this.Hide();
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



        private async void Sign_up_button_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            var aEmail = emailValidation.ValidateEmail(Sign_up_email.Text);
            if (!aEmail.IsValid)
            {
                label1.Text = aEmail.Message;
                return;
            }
            if (Sign_up_pass.Text != Sign_up_pass2.Text) { label1.Text = "Pass is not corect"; return; }
            var aPass = passValidation.ValidatePassword(Sign_up_pass.Text);
            if (!aPass.IsValid)
            {

                label1.Text = aPass.Message;
                return;
            }

            label1.Text = "";


            try
            {
                string url = "http://localhost:3001/api/auth/Regists";
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
                        //TestForm form1 = new TestForm();
                        //Authorization _a = new Authorization();
                        //form1.Show();
                        //_a.Hide();
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
        private async void Sign_up_pass2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Sign_up_pass_TextChanged(object sender, EventArgs e)
        {
           
        }

        private async void Sign_in_pass_TextChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region WorkWithEmail
        private async void Sign_in_email_TextChanged(object sender, EventArgs e)
        {
        }


        private async void Sign_up_email_TextChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region Placeholder
        private void SetEmailPlaceholder(TextBox textBox, string placeholderText)
        {

        }

        private void SetPasswordPlaceholder(TextBox textBox, string placeholderText)
        {

        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "Email" || textBox.Text == "Password" || textBox.Text == "Repeat Password")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Gray;
                    if (textBox == Sign_up_pass || textBox == Sign_up_pass2 || textBox == Sign_in_pass)
                    {
                        textBox.UseSystemPasswordChar = true;
                    }
                }
            }
        }
        #endregion

        #region WorkWithCheck
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Sign_up_pass.UseSystemPasswordChar = false;
                Sign_up_pass2.UseSystemPasswordChar = false;
            }
            else
            {
                Sign_up_pass.UseSystemPasswordChar = true;
                Sign_up_pass2.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Sign_in_pass.UseSystemPasswordChar = false;
            }
            else
            {
                Sign_in_pass.UseSystemPasswordChar = true;
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private bool ArePasswordsMatching()
        {
            return Sign_up_pass.Text == Sign_up_pass2.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
    }
}
