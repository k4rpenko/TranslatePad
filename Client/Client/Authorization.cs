using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Client
{

    public partial class Authorization : Form
    {
        private bool isEmailValidSignUp = false;
        private bool isPasswordValidSignUp = false;
        private bool isEmailValidSignIn = false;
        private bool isPasswordValidSignIn = false;
        private bool buttonClicked = false;





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
            buttonClicked = true;

            if (!isEmailValidSignIn)
            {
                ShowError2("Уведіть правильну адресу електронної пошти");
            }
            else
            {
                label2.Visible = false;
                // Логіка входу, наприклад, перевірка користувача, виклик API і т.д.
            }
        }



        private async void Sign_up_button_Click(object sender, EventArgs e)
        {

            //Font buttonFont = new Font("Arial", 10, FontStyle.Bold); // Наприклад, Arial шрифт, розмір 12, жирний стиль

            //// Встановлення шрифту для кнопки "Sign in"
            //Sign_up_button.Font = buttonFont;

            if (!isEmailValidSignUp)
            {
                ShowError("Уведіть правильну адресу електронної пошти");
            }
            else if (!isPasswordValidSignUp)

            {
                if (isPasswordValidSignUp)
                {
                    label2.Visible = false;
                }
                else
                {
                    ShowError("Пароль не співпадає");
                }

                if (buttonClicked && !isPasswordValidSignUp)
                {
                    ShowError("Мінімум 8 символів, одну велику літеру, одну малу літеру та одну цифру");
                }
                else
                {
                    label1.Visible = false;
                }
            }
            else
            {
                label1.Visible = false;
                // Логіка реєстрації, наприклад, збереження даних, виклик API і т.д.
            }
        }
        #endregion

        #region WorkWithPassword
        private async void Sign_up_pass2_TextChanged(object sender, EventArgs e)
        {

            Sign_up_pass2.ForeColor = Color.White;

            isPasswordValidSignUp = ArePasswordsMatching();

            //if (isPasswordValidSignUp)
            //{
            //    label2.Visible = false;
            //}
            //else
            //{
            //    ShowError("Пароль не співпадає");
            //}
        }

        private async void Sign_up_pass_TextChanged(object sender, EventArgs e)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            isPasswordValidSignUp = Regex.IsMatch(Sign_up_pass.Text, passwordPattern);
            Sign_up_pass.ForeColor = Color.White;

            //if (buttonClicked && !isPasswordValidSignUp)
            //{
            //    ShowError("Мінімум 8 символів, одну велику літеру, одну малу літеру та одну цифру");
            //}
            //else
            //{
            //    label1.Visible = false;
            //}
        }

        private async void Sign_in_pass_TextChanged(object sender, EventArgs e)
        {
            Sign_in_pass.ForeColor = Color.White;
            // Логіка для обробки зміни тексту пароля при вході
        }
        #endregion

        #region WorkWithEmail
        private async void Sign_in_email_TextChanged(object sender, EventArgs e)
        {

            Sign_in_email.ForeColor = Color.White;
            string emailPattern = @"^[^@\s]+@[^\s@]+\.[^\s@]+$";
            isEmailValidSignIn = Regex.IsMatch(Sign_in_email.Text, emailPattern);

            if (buttonClicked && !isEmailValidSignIn)
            {
                ShowError2("Уведіть правильну адресу електронної пошти");
            }
            else
            {
                label2.Visible = false;
            }
        }








        private async void Sign_up_email_TextChanged(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^\s@]+\.[^\s@]+$";
            isEmailValidSignUp = Regex.IsMatch(Sign_up_email.Text, emailPattern);
            Sign_up_email.ForeColor = Color.White;

            //if (buttonClicked && !isEmailValidSignUp)
            //{
            //    ShowError("Уведіть правильну адресу електронної пошти");
            //}
            //else
            //{
            //    label1.Visible = false;
            //}
        }
        #endregion

        #region Placeholder
        private void SetEmailPlaceholder(TextBox textBox, string placeholderText)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void SetPasswordPlaceholder(TextBox textBox, string placeholderText)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
                textBox.UseSystemPasswordChar = false;
            }
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
            label2.Text = message;
            label2.ForeColor = Color.Red;
            label2.Visible = true;
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

    }
}
