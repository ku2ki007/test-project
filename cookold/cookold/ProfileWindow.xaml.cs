using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cookold
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainAppWindow mainWindow = new MainAppWindow();
            mainWindow.Show();
            this.Close();
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            FavoriteWindow favoritesWindow = new FavoriteWindow();
            favoritesWindow.Show();
            this.Close();
        }

        private void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow();
            createWindow.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже на странице профиля", "Профиль",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = NameTextBox.Text;
            string newEmail = EmailTextBox.Text;
            string newPhone = PhoneTextBox.Text;
            string newPassword = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Заглушка: Имя пользователя не может быть пустым",
                              "Валидация (заглушка)",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(newEmail))
            {
                MessageBox.Show("Заглушка: Email не может быть пустым",
                              "Валидация (заглушка)",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!string.IsNullOrWhiteSpace(newPassword) && newPassword != confirmPassword)
            {
                MessageBox.Show("Заглушка: Пароли не совпадают",
                              "Валидация (заглушка)",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            UserName.Text = newName;

            MessageBox.Show($"ЗАГЛУШКА: Профиль не сохранен, так как база данных отсутствует\n\n" +
                          $"Были введены следующие данные:\n" +
                          $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                          $"📝 Имя: {newName}\n" +
                          $"📧 Email: {newEmail}\n" +
                          $"📞 Телефон: {newPhone}\n" +
                          $"🔒 Пароль: {(string.IsNullOrWhiteSpace(newPassword) ? "не изменен" : "изменен (заглушка)")}\n" +
                          $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                          $"💡 Подсказка: При подключении базы данных эти данные будут сохраняться.",
                          "Заглушка - Сохранение не выполнено",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);

            PasswordBox.Clear();
            ConfirmPasswordBox.Clear();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из профиля?",
                                                     "Выход",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void CreateListButton_Click(object sender, RoutedEventArgs e)
        {
            CreateListWindow createListWindow = new CreateListWindow();
            createListWindow.Show();
            this.Close();
        }
    }
}