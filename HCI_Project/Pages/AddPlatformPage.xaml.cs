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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.Pages
{
    /// <summary>
    /// Interaction logic for AddPlatformPage.xaml
    /// </summary>
    public partial class AddPlatformPage : Page
    {
        public AddPlatformPage()
        {
            InitializeComponent();
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim().Equals("Input student name"))
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = Brushes.Black;
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim().Equals(string.Empty))
            {
                NameTextBox.Text = "Input student name";
                NameTextBox.Foreground = Brushes.LightSlateGray;
            }
        }

        private void CreateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
