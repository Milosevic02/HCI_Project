using HCI_Project.Helpers;
using HCI_Project.Model;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for AddPlatformWindow.xaml
    /// </summary>
    public partial class AddPlatformWindow : Window
    {
        public AddPlatformWindow()
        {
            InitializeComponent();
            NameTextBox.Text = "Input platform name";
            NameTextBox.Foreground = Brushes.LightSlateGray;
            KOTextBox.Text = "Input knockout number";
            KOTextBox.Foreground = Brushes.LightSlateGray;
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);


        }

        private void KOTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void KOTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KOTextBox.Text.Trim().Equals("Input knockout number"))
            {
                KOTextBox.Text = "";
                KOTextBox.Foreground = Brushes.Black;
            }
        }

        private void KOTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (KOTextBox.Text.Trim().Equals(string.Empty))
            {
                KOTextBox.Text = "Input knockout number";
                KOTextBox.Foreground = Brushes.LightSlateGray;
            }
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim().Equals("Input platform name"))
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = Brushes.Black;
            }

        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim().Equals(string.Empty))
            {
                NameTextBox.Text = "Input platform name";
                NameTextBox.Foreground = Brushes.LightSlateGray;
            }
        }

        private bool ValidateFormData()
        {
            bool isValid = true;
            string richText = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd).Text.Trim();


            if (NameTextBox.Text.Trim().Equals(string.Empty) || NameTextBox.Text.Trim().Equals("Input platform name"))
            {
                isValid = false;
                NameErrorLabel.Content = "Form field cannot be left empty!";
                NameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                NameErrorLabel.Content = string.Empty;
                NameTextBox.BorderBrush = Brushes.Gray;
            }

            if (KOTextBox.Text.Trim().Equals(string.Empty) || KOTextBox.Text.Trim().Equals("Input knockout number"))
            {
                isValid = false;
                KOErrorLabel.Content = "Form field cannot be left empty!";
                KOTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                KOErrorLabel.Content = string.Empty;
                KOTextBox.BorderBrush = Brushes.Gray;
            }

            if (richText.Equals(string.Empty))
            {
                isValid = false;
                NameErrorLabel.Content = "RichTextBox field cannot be left empty!";
                NameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                NameErrorLabel.Content = string.Empty;
                NameTextBox.BorderBrush = Brushes.Gray;
            }


            return isValid;
        }

            private void EditorRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontWeight = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold));

            object fontFamily = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = fontFamily;
        }

        private void EditorRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (ValidateFormData())
            {
                StreamingPlatform platform = new StreamingPlatform();

                mainWindow.Students.Add(newStudent);

                mainWindow.ShowToastNotification(new ToastNotification("Success", "Student added to the Data Table", NotificationType.Success));

                this.NavigationService.Navigate(new Uri("Pages/DataTablePage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                mainWindow.ShowToastNotification(new ToastNotification("Error", "Form fields are not correctly filled!", NotificationType.Error));
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null && !EditorRichTextBox.Selection.IsEmpty)
            {
                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
