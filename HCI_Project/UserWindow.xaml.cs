using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    /// 
    public partial class UserWindow : Window
    {

        public ObservableCollection<StreamingPlatform> platforms = new ObservableCollection<StreamingPlatform>();



        public UserWindow(User user)
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPlatformWindow addPlatformWindow = new AddPlatformWindow();
            addPlatformWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
