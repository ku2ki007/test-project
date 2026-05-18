using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace cookold
{
    public partial class RecipeWindow : Window
    {
        public RecipeWindow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainAppWindow mainWindow = new MainAppWindow();
            mainWindow.Show();
            this.Close();
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            FavoriteWindow favWindow = new FavoriteWindow();
            favWindow.Show();
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
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private bool _isInFavorites = false;

        private void AddToFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            _isInFavorites = !_isInFavorites;

            var button = sender as Button;
            var heartIcon = FindVisualChild<TextBlock>(button, "HeartIcon");
            var buttonText = FindVisualChild<TextBlock>(button, "ButtonText");

            if (_isInFavorites)
            {
                heartIcon.Text = "❤️";
                buttonText.Text = "В избранном";
                MessageBox.Show("Рецепт добавлен в избранное!", "Избранное",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                heartIcon.Text = "🤍";
                buttonText.Text = "В избранное";
                MessageBox.Show("Рецепт удален из избранного!", "Избранное",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element && element.Name == name)
                    return element;

                var result = FindVisualChild<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}