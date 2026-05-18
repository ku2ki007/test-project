using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cookold
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckFields();
        }

        private void CheckFields()
        {
            LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(LoginTextBox.Text) &&
                                     !string.IsNullOrWhiteSpace(PasswordBox.Password);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow RegisterWindow = new RegisterWindow();
            RegisterWindow.Show();
            this.Close();
        }
    }
}