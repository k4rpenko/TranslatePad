using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Authorization : Form
    {
        
        public Authorization()
        {
            InitializeComponent();
        }

        #region WorkWithButton
        private async void Sign_in_button_Click(object sender, EventArgs e)
        {
            buttonClicked = true; // Позначаємо, що кнопка була натиснута

            if (!isEmailValid && buttonClicked)
            {
                ShowError2("Уведіть правильну адресу електронної пошти");
            }
            else if (!isPasswordValid && buttonClicked)
            {
                ShowError2("Пароль має містити мінімум 8 символів, одну велику літеру, одну малу літеру та одну цифру");
            }
            else
            {
                // Продовжуємо з подальшою логікою реєстрації
                // Наприклад, збереження даних, виклик API і т.д.
                label2.Visible = false; // Приховати помилку якщо формат правильний
            }
        }

        private bool buttonClicked = false;
        private async void Sign_up_button_Click(object sender, EventArgs e)
        {
            buttonClicked = true; // Позначаємо, що кнопка була натиснута

            if (!isEmailValid && buttonClicked)
            {
                ShowError("Уведіть правильну адресу електронної пошти");
            }
            else if (!isPasswordValid && buttonClicked)
            {
                ShowError("Пароль має містити мінімум 8 символів, одну велику літеру, одну малу літеру та одну цифру");
            }
            else
            {
                // Продовжуємо з подальшою логікою реєстрації
                // Наприклад, збереження даних, виклик API і т.д.
                label1.Visible = false; // Приховати помилку якщо формат правильний
            }
        }

        #endregion

        #region WorkWithPassword

        private async void Sign_up_pass2_TextChanged(object sender, EventArgs e)
        {
            string password1 = Sign_up_pass.Text;
            string password2 = Sign_up_pass2.Text;

            if (password1 == password2)
            {
                // Паролі співпадають
                // Можна виконати додаткові дії, наприклад, приховати помилку про неправильний повтор пароля
                label2.Visible = false; // Приховуємо помилку, якщо паролі співпадають
            }
            else
            {
                ShowError("Пароль не співпадає");
            }
        }
        private bool isPasswordValid = false;
        private async void Sign_up_pass_TextChanged(object sender, EventArgs e)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            isPasswordValid = Regex.IsMatch(Sign_up_pass.Text, passwordPattern);

            if (buttonClicked && !isPasswordValid)
            {
                ShowError("Пароль має містити мінімум 8 символів, одну велику літеру, одну малу літеру та одну цифру");
            }
            else
            {
                label1.Visible = false; // Приховуємо помилку, якщо пароль валідний
            }
        }

        private async void Sign_in_pass_TextChanged(object sender, EventArgs e)
        {


        }
        #endregion

        #region WorkWithEmail
        private async void Sign_in_email_TextChanged(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^\s@]+\.[^\s@]+$";
            isEmailValid = Regex.IsMatch(Sign_up_email.Text, emailPattern);

            if (buttonClicked && !isEmailValid)
            {
                ShowError2("Уведіть правильну адресу електронної пошти");
            }
            else
            {
                label2.Visible = false; // Приховуємо помилку, якщо емейл валідний
            }
        }



        private bool isEmailValid = false;
        private async void Sign_up_email_TextChanged(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^\s@]+\.[^\s@]+$";
            isEmailValid = Regex.IsMatch(Sign_up_email.Text, emailPattern);

            if (buttonClicked && !isEmailValid)
            {
                ShowError("Уведіть правильну адресу електронної пошти");
            }
            else
            {
                label1.Visible = false; // Приховуємо помилку, якщо емейл валідний
            }



        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Якщо чекбокс позначений, властивість UseSystemPasswordChar вимкнена
            if (checkBox1.Checked)
            {
                Sign_up_pass.UseSystemPasswordChar = false;
                Sign_up_pass2.UseSystemPasswordChar = false;
            }
            else
            {
                // Якщо чекбокс не позначений, властивість UseSystemPasswordChar увімкнена
                Sign_up_pass.UseSystemPasswordChar = true;
                Sign_up_pass2.UseSystemPasswordChar = true;
            }
        }


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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Якщо чекбокс позначений, властивість UseSystemPasswordChar вимкнена
            if (checkBox2.Checked)
            {
                Sign_in_pass.UseSystemPasswordChar = false;
            }
            else
            {
                // Якщо чекбокс не позначений, властивість UseSystemPasswordChar увімкнена
                Sign_in_pass.UseSystemPasswordChar = true;

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



    }
}
