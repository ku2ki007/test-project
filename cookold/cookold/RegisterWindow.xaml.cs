using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace cookold
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regemail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regemail.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void CheckFields()
        {
            bool isValid = true;

            if (!IsValidEmail(EmailTextBox.Text))
            {
                EmailErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                EmailErrorText.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || LoginTextBox.Text.Length < 3)
            {
                LoginErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                LoginErrorText.Visibility = Visibility.Collapsed;
            }

            string password = PasswordBox.Password;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                PasswordErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                PasswordErrorText.Visibility = Visibility.Collapsed;
            }

            string confirmPassword = ConfirmPasswordBox.Password;
            if (string.IsNullOrWhiteSpace(confirmPassword) || password != confirmPassword)
            {
                ConfirmPasswordErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                ConfirmPasswordErrorText.Visibility = Visibility.Collapsed;
            }

            RegisterButton.IsEnabled = isValid;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
            ErrorText.Visibility = Visibility.Collapsed;
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
            ErrorText.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckFields();
            ErrorText.Visibility = Visibility.Collapsed;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckFields();
            ErrorText.Visibility = Visibility.Collapsed;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoginLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}