using HCI_Project.Helpers;
using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string username = parts[0];
                    string password = parts[1];
                    bool isAdmin = parts[2] == "1";
                    if(isAdmin)
                    {
                        User user = new User(username, password, UserRole.Admin);
                        users.Add(user);
                    }
                    else
                    {
                        User user = new User(username, password, UserRole.Visitor);
                        users.Add(user);
                    }
                }
            }

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in users)
            {
                if (user.Username == UsernameTextBox.Text && user.Password == PasswordBox.Password)
                {
                    UsernameErrorLabel.Content = string.Empty;
                    UsernameTextBox.BorderBrush = Brushes.Gray;
                    PasswordErrorLabel.Content = string.Empty;
                    PasswordBox.BorderBrush = Brushes.Gray;
                    if (user.Role == UserRole.Admin)
                    {
                        NavigateToPage("AdminPage");
                    }
                    else
                    {
                        NavigateToPage("VisitorPage");
                    }
                }
                else
                {
                    UsernameErrorLabel.Content = "Wrong input";
                    UsernameTextBox.BorderBrush = Brushes.Red;
                    PasswordErrorLabel.Content = "Wrong input";
                    PasswordBox.BorderBrush = Brushes.Red;
                }
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NavigateToPage(string pageName)
        {
            String pageUri = "Pages/" + pageName + ".xaml";
            MainFrame.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
