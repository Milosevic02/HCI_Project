using HCI_Project.Helpers;
using HCI_Project.Model;
using Microsoft.Win32;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        public ChangeWindow changeWindow;
        private NotificationManager notificationManager;
        public string SelfPicture = "";
        public string name;
        public StreamingPlatform oldPlatform;
        public int indeks;
        private DataIO serializer = new DataIO();

        public ChangeWindow(StreamingPlatform platform)
        {
            InitializeComponent();
            oldPlatform = platform;
            notificationManager = new NotificationManager();
            changeWindow = this;
            NameTextBox.Text = platform.Name;
            SelfPicture = platform.Image;
            name = platform.Name;
            QuantityTextBox.Text = platform.Rating.ToString();
            EditorRichTextBox.Document = platform.ReadFromRTF();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(platform.Image, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageControl.Source = bitmap;
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for (int i = 8; i <= 34; i += 2)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;
                FontSizeComboBox.Items.Add(item);
            }
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
        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void QuantityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

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

            if (QuantityTextBox.Text.Trim().Equals(string.Empty) || QuantityTextBox.Text.Trim().Equals("Input knockout number"))
            {
                isValid = false;
                QuantityErrorLabel.Content = "Form field cannot be left empty!";
                QuantityTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                QuantityErrorLabel.Content = string.Empty;
                QuantityTextBox.BorderBrush = Brushes.Gray;
            }

            if (SelfPicture.Equals(string.Empty))
            {
                isValid = false;
                this.ShowToastNotification(new ToastNotification("Error", "You must choose a image!", NotificationType.Error));


            }

            if (EditorRichTextBox.Document.Blocks.Count == 0)
            {
                isValid = false;
                this.ShowToastNotification(new ToastNotification("Error", "You must type a description!", NotificationType.Error));

            }


            return isValid;
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
        private void SaveDataAsXML()
        {
            serializer.SerializeObject<ObservableCollection<StreamingPlatform>>(UserWindow.userWindow.Platforms, "Platforms.xml");
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)FontSizeComboBox.SelectedItem;
                double selectedFontSize = Convert.ToDouble(selectedItem.Content);

                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, selectedFontSize);
            }
        }

        private void EditorRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontWeight = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty); //FontWeightProperty - koristi se za debljinu teksta
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold)); //postavlja isChecked toogleButton-a na true ili false

            object fontStyle = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty); //FontStyle - se koristi za postavljanje stila fonta teksta
            ItalicToggleButton.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(FontStyles.Italic));//postavlja isChecked toogleButton-a na true ili false

            object isUnderlined = EditorRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty); //TextDecorations - se koristi za postavljanje dekoracija teksta
            UnderlineToggleButton.IsChecked = (isUnderlined != DependencyProperty.UnsetValue) && ((TextDecorationCollection)isUnderlined).Any(d => d.Location == TextDecorationLocation.Underline);//postavlja isChecked toogleButton-a na true ili false

            object fontFamily = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty); //Font Family
            FontFamilyComboBox.SelectedItem = fontFamily;


            object foreground = EditorRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty); //Color
            SolidColorBrush foregroundBrush = foreground as SolidColorBrush;
            if (foregroundBrush != null)
            {
                ColorComboBox.SelectedItem = ColorComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => ((SolidColorBrush)item.Background).Color == foregroundBrush.Color);
            }


            object fontSize = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty); //Font Size
            double fontSizeValue;
            if (double.TryParse(fontSize.ToString(), out fontSizeValue))
            {
                ComboBoxItem fontSizeItem = FontSizeComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => Convert.ToDouble(item.Content) == fontSizeValue);
                FontSizeComboBox.SelectedItem = fontSizeItem;
            }
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

        private void EditorRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WordCounter(EditorRichTextBox.Document);

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

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(oldPlatform.Name != NameTextBox.Text)
            {
                if (ValidateFormData())
                {
                    if (IsExisting(NameTextBox.Text))
                    {
                        DeleteOldRtfFile();
                        StreamingPlatform newPlatform = new StreamingPlatform(Int32.Parse(QuantityTextBox.Text), NameTextBox.Text, SelfPicture);
                        newPlatform.SaveAsRTF(EditorRichTextBox.Document);
                        int ind = UserWindow.userWindow.Platforms.IndexOf(oldPlatform);
                        UserWindow.userWindow.Platforms[ind] = newPlatform;
                        SaveDataAsXML();
                        UserWindow.userWindow.ShowToastNotification(new ToastNotification("Success", "Platform added to the Data Table", NotificationType.Success));
                        this.Hide();
                        UserWindow.userWindow.ShowDialog();
                    }
                    else
                    {
                        this.ShowToastNotification(new ToastNotification("Error", "Platform with this name already exists", NotificationType.Error));


                    }
                }
                else
                {
                    this.ShowToastNotification(new ToastNotification("Error", "Form fields are not correctly filled!", NotificationType.Error));

                }


            }
            else
            {
                DeleteOldRtfFile();
                StreamingPlatform newPlatform = new StreamingPlatform(Int32.Parse(QuantityTextBox.Text), NameTextBox.Text, SelfPicture);
                newPlatform.SaveAsRTF(EditorRichTextBox.Document);
                int ind = UserWindow.userWindow.Platforms.IndexOf(oldPlatform);
                UserWindow.userWindow.Platforms[ind] = newPlatform;
                SaveDataAsXML();
                UserWindow.userWindow.ShowToastNotification(new ToastNotification("Success", "Platform added to the Data Table", NotificationType.Success));
                this.Hide();
                UserWindow.userWindow.ShowDialog();
            }



        }


        private bool IsExisting(string name)
        {
            foreach (StreamingPlatform platform in UserWindow.userWindow.Platforms)
            {
                if (platform.Name == name)
                {
                    return false;
                }
            }
            return true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserWindow.userWindow.ShowDialog();
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title, toastNotification.Message, toastNotification.Type, "ChangeNotificationArea");
        }
        private void DeleteOldRtfFile()
        {
            string path = (name + ".rtf").ToString();
            File.Delete(path);
        }
    }
}
