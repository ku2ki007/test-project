using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace cookold
{
    public partial class CreateWindow : Window
    {
        private int _stepCount = 0;
        private List<StepControl> _steps = new List<StepControl>();

        public CreateWindow()
        {
            InitializeComponent();
            AddDefaultIngredient();
        }

        private void AddDefaultIngredient()
        {
            AddIngredientRow();
        }

        private void AddIngredientRow()
        {
            Border ingredientBorder = new Border
            {
                Style = (Style)FindResource("IngredientItemStyle")
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            TextBox ingredientTextBox = new TextBox
            {
                Style = (Style)FindResource("PlaceholderTextBox"),
                Height = 30,
                Margin = new Thickness(0, 0, 10, 0),
                Text = "",
                Tag = "Название ингредиента..."
            };
            ingredientTextBox.SetValue(Grid.ColumnProperty, 0);

            TextBox quantityTextBox = new TextBox
            {
                Style = (Style)FindResource("PlaceholderTextBox"),
                Height = 30,
                Width = 120,
                Margin = new Thickness(0, 0, 10, 0),
                Text = "",
                Tag = "Количество..."
            };
            quantityTextBox.SetValue(Grid.ColumnProperty, 1);

            Button removeButton = new Button
            {
                Content = "🗑️",
                Style = (Style)FindResource("RemoveButtonStyle"),
                Tag = ingredientBorder
            };
            removeButton.SetValue(Grid.ColumnProperty, 2);
            removeButton.Click += RemoveIngredient_Click;

            grid.Children.Add(ingredientTextBox);
            grid.Children.Add(quantityTextBox);
            grid.Children.Add(removeButton);
            ingredientBorder.Child = grid;
            IngredientsPanel.Children.Add(ingredientBorder);
        }

        private void RemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag is Border ingredientBorder)
            {
                IngredientsPanel.Children.Remove(ingredientBorder);
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            _stepCount++;

            Border stepBorder = new Border
            {
                Style = (Style)FindResource("StepCardStyle"),
                Tag = _stepCount
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            Border numberBorder = new Border
            {
                Width = 50,
                Height = 50,
                CornerRadius = new CornerRadius(25),
                Margin = new Thickness(0, 0, 15, 0),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#556B2F"))
            };
            TextBlock numberText = new TextBlock
            {
                Text = _stepCount.ToString(),
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            numberBorder.Child = numberText;
            numberBorder.SetValue(Grid.ColumnProperty, 0);

            StackPanel stepPanel = new StackPanel();
            TextBox stepTitleBox = new TextBox
            {
                Style = (Style)FindResource("PlaceholderTextBox"),
                Height = 35,
                Margin = new Thickness(0, 0, 0, 5),
                Text = "",
                Tag = "Название шага..."
            };

            TextBox stepDescriptionBox = new TextBox
            {
                Style = (Style)FindResource("PlaceholderTextArea"),
                Height = 60,
                Text = "",
                Tag = "Описание шага приготовления..."
            };

            stepPanel.Children.Add(stepTitleBox);
            stepPanel.Children.Add(stepDescriptionBox);
            stepPanel.SetValue(Grid.ColumnProperty, 1);
            stepPanel.Margin = new Thickness(0, 0, 15, 0);

            Border photoBorder = new Border
            {
                Width = 80,
                Height = 80,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0")),
                CornerRadius = new CornerRadius(8),
                Cursor = Cursors.Hand
            };
            TextBlock photoText = new TextBlock
            {
                Text = "📷",
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            photoBorder.Child = photoText;
            int currentStepNumber = _stepCount;
            photoBorder.MouseLeftButtonDown += (s, args) => UploadStepImage(s, args, currentStepNumber);
            photoBorder.SetValue(Grid.ColumnProperty, 2);

            Button removeButton = new Button
            {
                Content = "🗑️",
                Style = (Style)FindResource("RemoveButtonStyle"),
                Width = 30,
                Height = 30,
                Margin = new Thickness(5, 0, 0, 0),
                Tag = stepBorder
            };
            removeButton.SetValue(Grid.ColumnProperty, 3);
            removeButton.Click += RemoveStep_Click;

            grid.Children.Add(numberBorder);
            grid.Children.Add(stepPanel);
            grid.Children.Add(photoBorder);
            grid.Children.Add(removeButton);
            stepBorder.Child = grid;
            StepsPanel.Children.Add(stepBorder);

            _steps.Add(new StepControl { Border = stepBorder, Number = _stepCount });
        }

        private void RemoveStep_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag is Border stepBorder)
            {
                StepsPanel.Children.Remove(stepBorder);
                _steps.RemoveAll(s => s.Border == stepBorder);
                RenumberSteps();
            }
        }

        private void RenumberSteps()
        {
            int i = 1;
            foreach (StepControl step in _steps)
            {
                step.Number = i;
                Border stepBorder = step.Border;
                Grid grid = stepBorder.Child as Grid;
                if (grid != null && grid.Children[0] is Border numberBorder && numberBorder.Child is TextBlock numberText)
                {
                    numberText.Text = i.ToString();
                }
                i++;
            }
            _stepCount = _steps.Count;
        }

        private void UploadStepImage(object sender, MouseButtonEventArgs e, int stepNumber)
        {
            MessageBox.Show($"Загрузить фото для шага {stepNumber}", "Загрузка фото",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UploadImage_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Выберите изображение для блюда", "Загрузка фото",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientRow();
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string title = RecipeTitleTextBox.Text;
            string shortDesc = ShortDescriptionTextBox.Text;
            string fullDesc = FullDescriptionTextBox.Text;

            ComboBoxItem categoryItem = CategoryComboBox.SelectedItem as ComboBoxItem;
            string category = categoryItem != null ? categoryItem.Content.ToString() : "";

            ComboBoxItem kitchenItem = KitchenComboBox.SelectedItem as ComboBoxItem;
            string kitchen = kitchenItem != null ? kitchenItem.Content.ToString() : "";

            ComboBoxItem timeItem = TimeComboBox.SelectedItem as ComboBoxItem;
            string time = timeItem != null ? timeItem.Content.ToString() : "";

            List<string> ingredients = new List<string>();
            foreach (Border ingredientBorder in IngredientsPanel.Children)
            {
                if (ingredientBorder.Child is Grid grid && grid.Children.Count >= 2)
                {
                    TextBox ingredientBox = grid.Children[0] as TextBox;
                    TextBox quantityBox = grid.Children[1] as TextBox;
                    if (!string.IsNullOrWhiteSpace(ingredientBox?.Text))
                    {
                        string quantityText = quantityBox != null && !string.IsNullOrWhiteSpace(quantityBox.Text) ? quantityBox.Text : "";
                        ingredients.Add($"{ingredientBox.Text}{(string.IsNullOrWhiteSpace(quantityText) ? "" : $" - {quantityText}")}");
                    }
                }
            }

            List<string> steps = new List<string>();
            foreach (StepControl step in _steps)
            {
                if (step.Border.Child is Grid grid && grid.Children[1] is StackPanel stepPanel)
                {
                    if (stepPanel.Children.Count >= 2 && stepPanel.Children[0] is TextBox titleBox && stepPanel.Children[1] is TextBox descBox)
                    {
                        string titleText = string.IsNullOrWhiteSpace(titleBox.Text) ? "" : titleBox.Text;
                        string descText = string.IsNullOrWhiteSpace(descBox.Text) ? "" : descBox.Text;

                        if (!string.IsNullOrWhiteSpace(titleText) || !string.IsNullOrWhiteSpace(descText))
                        {
                            steps.Add($"{step.Number}. {titleText}{(string.IsNullOrWhiteSpace(descText) ? "" : $": {descText}")}");
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Пожалуйста, введите название рецепта", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ingredients.Count == 0)
            {
                MessageBox.Show("Пожалуйста, добавьте хотя бы один ингредиент", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (steps.Count == 0)
            {
                MessageBox.Show("Пожалуйста, добавьте хотя бы один шаг приготовления", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Рецепт \"{title}\" успешно сохранен!\n\nКатегория: {category}\nКухня: {kitchen}\nВремя: {time}\nИнгредиентов: {ingredients.Count}\nШагов: {steps.Count}",
                          "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MessageBox.Show("Вы уже на странице создания рецепта", "Создание рецепта",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }
    }

    public class StepControl
    {
        public Border Border { get; set; }
        public int Number { get; set; }
    }
}