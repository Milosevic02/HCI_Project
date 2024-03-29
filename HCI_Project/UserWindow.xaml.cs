using HCI_Project.Helpers;
using HCI_Project.Model;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
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

        public ObservableCollection<StreamingPlatform> Platforms;
        public static UserWindow userWindow;
        private DataIO serializer = new DataIO();
        private NotificationManager notificationManager;
        private bool isAdmin = true;
        private StreamingPlatform SelectedPlatform;


        public UserWindow(User user)
        {
            InitializeComponent();
            SelectedPlatform = null;
            if (user.Role == UserRole.Visitor) { 
                isAdmin = false;
                AddButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
            }
            notificationManager = new NotificationManager();
            userWindow = this;
            userWindow.ShowToastNotification(new ToastNotification("Success", "Logged in", NotificationType.Success));
            Platforms = serializer.DeSerializeObject<ObservableCollection<StreamingPlatform>>("Platforms.xml");
            if (Platforms == null)
            {
                Platforms = new ObservableCollection<StreamingPlatform>();
            }
            foreach(StreamingPlatform platform in Platforms)
            {
                Console.WriteLine(platform.Name);
            }
            DataContext = this;

            PlatformsDataGrid.ItemsSource = Platforms;

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                userWindow.Hide();
                DisplayWindow displayWindow = new DisplayWindow(SelectedPlatform);  

                displayWindow.ShowDialog();

            }
            else
            {
                userWindow.Hide();
                ChangeWindow changeWindow = new ChangeWindow(SelectedPlatform);
                changeWindow.ShowDialog();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPlatformWindow addPlatformWindow = new AddPlatformWindow();
            userWindow.Hide();
            addPlatformWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            {
                int count = 0;
                foreach (StreamingPlatform p in Platforms)
                {
                    if (p.IsChecked)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    userWindow.ShowToastNotification(new ToastNotification("Error", "Please check player to delete", NotificationType.Error));
                }

                List<StreamingPlatform> Temp = Platforms.ToList();
                foreach (StreamingPlatform ap in Temp)
                {
                    if (ap.IsChecked)
                    {
                        this.Platforms.Remove(ap);

                        string filePath = ap.Description;
                        if (File.Exists(filePath))
                        {

                            File.Delete(filePath);

                        }
                    }
                }

            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveDataAsXML()
        {
            serializer.SerializeObject<ObservableCollection<StreamingPlatform>>(Platforms, "Platforms.xml");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                SaveDataAsXML();
                this.Hide();
                MainWindow.mainWindow.ShowDialog();

            }
            else
            {
                e.Cancel = true;
            }
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title, toastNotification.Message, toastNotification.Type, "UserNotificationArea");
        }

        private void PlatformsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedPlatform = (StreamingPlatform)PlatformsDataGrid.SelectedItem;
            }
            catch
            {
                SelectedPlatform = null;
            }
        }
    }
}
