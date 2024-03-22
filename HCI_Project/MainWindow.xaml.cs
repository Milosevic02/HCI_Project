using HCI_Project.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataIO serializer = new DataIO();
        public List<User>Users = new List<User>();
        public List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            User visitor = new User("visitor", "visitor123", UserRole.Visitor);
            User admin = new User("admin", "admin123", UserRole.Admin);
            users.Add(visitor);
            users.Add(admin);   
            serializer.SerializeObject<List<User>>(users,"Users.xaml");
            Users = serializer.DeSerializeObject<List<User>>("Users.xaml");

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in users)
            {
                if (user.Username == UsernameTextBox.Text && user.Password == PasswordBox.Password)
                {
                    UsernameErrorLabel.Content = string.Empty;
                    UsernameErrorLabel.BorderBrush = Brushes.Gray;
                    PasswordErrorLabel.Content = string.Empty;
                    PasswordErrorLabel.BorderBrush = Brushes.Gray;
                    if (user.Role == UserRole.Admin)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    UsernameErrorLabel.Content = "Wrong input";
                    UsernameTextBox.BorderBrush = Brushes.Red;
                    PasswordErrorLabel.Content = "Wrong input";
                    PasswordErrorLabel.BorderBrush = Brushes.Red;
                }
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
