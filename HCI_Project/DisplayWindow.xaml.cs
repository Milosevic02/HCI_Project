using HCI_Project.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow : Window
    {
        public static DisplayWindow displayWindow;

        public DisplayWindow(StreamingPlatform platform)
        {
            InitializeComponent();
            displayWindow = this;
            NameTextBlock.Text = platform.Name;
            UsersTextBlock.Text = platform.Rating.ToString();
            EditorRichTextBox.Document = platform.ReadFromRTF();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(platform.Image, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageControl.Source = bitmap;
            DateTextBlock.Text = platform.Date.ToString();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserWindow.userWindow.ShowDialog();
        }
    }
}
