using HCI_Project.Helpers;
using HCI_Project.Model;
using Microsoft.Win32;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
        private NotificationManager notificationManager;
        public string SelfPicture= "";


        public AddPlatformWindow()
        {
            InitializeComponent();
            notificationManager = new NotificationManager();
            NameTextBox.Text = "Input platform name";
            NameTextBox.Foreground = Brushes.LightSlateGray;
            KOTextBox.Text = "Input knockout number";
            KOTextBox.Foreground = Brushes.LightSlateGray;
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            List<double> fontSizes = new List<double> { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32 };
            FontSizeComboBox.ItemsSource = fontSizes;
            InitializeColor();



        }

        private void InitializeColor()
        {
            foreach (PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color color = (Color)prop.GetValue(null, null);
                ComboBoxItem item = new ComboBoxItem();
                item.Content = prop.Name;
                item.Background = new SolidColorBrush(color);
                item.Width = 100;
                ColorComboBox.Items.Add(item);
            }
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

            if(SelfPicture.Equals(string.Empty))
            {
                isValid = false;
                this.ShowToastNotification(new ToastNotification("Error", "You must choose a image!", NotificationType.Error));


            }

            //if (richText.Equals(string.Empty))
            //{
            //    isValid = false;
            //    NameErrorLabel.Content = "RichTextBox field cannot be left empty!";
            //    NameTextBox.BorderBrush = Brushes.Red;
            //}
            //else
            //{
            //    NameErrorLabel.Content = string.Empty;
            //    NameTextBox.BorderBrush = Brushes.Gray;
            //}


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
            WordCounter(EditorRichTextBox.Document);
        }

        private void WordCounter(FlowDocument doc)
        {

            TextRange txtRange = new TextRange(doc.ContentStart, doc.ContentEnd);
            string txt = txtRange.Text;

            Regex regex = new Regex(@"\b\w+\b");


            int wordCount = regex.Matches(txt).Count;
            wordCountTextBlock.Text = wordCount.ToString();
            Console.WriteLine(wordCount.ToString());

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|.jpg;.jpeg;.png|" +
              "JPEG (.jpg;.jpeg)|.jpg;.jpeg|" +
              "Portable Network Graphic (.png)|*.png";
            if (op.ShowDialog() == true)
            {

                //PlayerImage.Source = new BitmapImage(new Uri(op.FileName));
                SelfPicture = new Uri(op.FileName).ToString();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(SelfPicture, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();

                ImageControl.Source = bitmap;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserWindow.userWindow.ShowDialog();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = UserWindow.userWindow;
            if (ValidateFormData())
            {
                StreamingPlatform platform = new StreamingPlatform(Int32.Parse(KOTextBox.Text),NameTextBox.Text,SelfPicture);
                platform.SaveAsRTF(EditorRichTextBox.Document);
                userWindow.Platforms.Add(platform);
                userWindow.ShowToastNotification(new ToastNotification("Success", "Platform added to the Data Table", NotificationType.Success));
                this.Hide();
                userWindow.ShowDialog();
            }
            else
            {
                this.ShowToastNotification(new ToastNotification("Error", "Form fields are not correctly filled!", NotificationType.Error));
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
            if (ColorComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)ColorComboBox.SelectedItem;
                string colorName = (string)selectedItem.Content;
                Color selectedColor = (Color)typeof(Colors).GetProperty(colorName).GetValue(null, null);

                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(selectedColor));
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null && !EditorRichTextBox.Selection.IsEmpty)
            {
                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBox.SelectedItem);
            }
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title, toastNotification.Message, toastNotification.Type, "AddNotificationArea");
        }
    }
}
