using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace cookold
{
    public partial class FavoriteWindow : Window
    {
        public FavoriteWindow()
        {
            InitializeComponent();
            UpdateFavoritesCount();
        }

        private void UpdateFavoritesCount()
        {
            FavoritesCount.Text = "3 рецепта";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Recipe_Click(object sender, MouseButtonEventArgs e)
        {
            RecipeWindow recipeWindow = new RecipeWindow();
            recipeWindow.Show();
            this.Close();
        }

        private void RemoveFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var parentBorder = FindParent<Border>(button);

            if (parentBorder != null && FavoritesPanel.Children.Contains(parentBorder))
            {
                MessageBoxResult result = MessageBox.Show("Удалить рецепт из избранного?",
                                                         "Удаление",
                                                         MessageBoxButton.YesNo,
                                                         MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    FavoritesPanel.Children.Remove(parentBorder);
                    UpdateFavoritesCount();

                    if (FavoritesPanel.Children.Count == 0)
                    {
                        EmptyFavoritesMessage.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainAppWindow mainWindow = new MainAppWindow();
            mainWindow.Show();
            this.Close();
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже на странице избранного", "Навигация",
                          MessageBoxButton.OK, MessageBoxImage.Information);
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


        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }
    }
}