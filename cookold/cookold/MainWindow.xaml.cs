using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cookold
{
    public partial class MainAppWindow : Window
    {
        public MainAppWindow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearSearchButton.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Collapsed
                : Visibility.Visible;

            string searchText = SearchTextBox.Text;

        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            SearchTextBox.Focus();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryFilter.SelectedIndex = 0;
            TimeFilter.SelectedIndex = 0;
            KitchenFilter.SelectedIndex = 0;
            SortFilter.SelectedIndex = 0;

            MessageBox.Show("Все фильтры очищены", "Фильтры",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            string category = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string time = (TimeFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string kitchen = (KitchenFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string sort = (SortFilter.SelectedItem as ComboBoxItem)?.Content.ToString();

            MessageBox.Show($"Настройки фильтров сохранены\nКатегория: {category}\nВремя: {time}\nКухня: {kitchen}\nСортировка: {sort}",
                          "Фильтры сохранены", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Recipe_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock recipeTitle = sender as TextBlock;
            if (recipeTitle != null)
            {
                RecipeWindow RecipeWindow = new RecipeWindow();
                RecipeWindow.Show();
                this.Close();
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже на главной странице", "Навигация",
                          MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void FilterRecipes(string searchText = null)
        {
            string category = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string time = (TimeFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string difficulty = (KitchenFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            string sort = (SortFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
        }
    }
}