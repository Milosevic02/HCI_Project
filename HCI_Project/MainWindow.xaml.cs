using HCI_Project.Helpers;
using HCI_Project.Model;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static MainWindow mainWindow = new MainWindow();

        public MainWindow()
        {
            InitializeComponent();
            users = ReadUsersFromFile();
            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("Images/bg-image.jpeg", UriKind.Relative));
            //UIPath = imageBrush;

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
                    UserWindow userWindow = new UserWindow(user);
                    this.Hide();
                    userWindow.ShowDialog();
                }
                else
                {
                    UsernameErrorLabel.Content = "Incorrect username or password";
                    UsernameTextBox.BorderBrush = Brushes.Red;
                    PasswordErrorLabel.Content = "Incorrect username or password";
                    PasswordBox.BorderBrush = Brushes.Red;
                }


            }
        }

        private List<User> ReadUsersFromFile()
        {
            List<User>retval = new List<User>();
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string username = parts[0];
                    string password = parts[1];
                    bool isAdmin = parts[2] == "1";
                    if (isAdmin)
                    {
                        User user = new User(username, password, UserRole.Admin);
                        retval.Add(user);
                    }
                    else
                    {
                        User user = new User(username, password, UserRole.Visitor);
                        retval.Add(user);
                    }
                }
                return retval;
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

