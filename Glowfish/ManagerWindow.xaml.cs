using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Glowfish {
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window {

        internal ClientList Clients;
        internal MainWindow mainWindow;

        internal ManagerWindow(ClientList Clients, MainWindow mainWindow) {
            InitializeComponent();

            this.Clients = Clients;
            this.mainWindow = mainWindow;
          
            Clients.Sort();
            ClientsListView.ItemsSource = Clients;
            this.Owner = mainWindow;
        }

        private void removeClientButton_Click(object sender, RoutedEventArgs e) {
            Client c = (Client)ClientsListView.SelectedItem;

            if(c != null) {
                MessageBoxResult result = MessageBox.Show("This will permanantly remove the client and all associated data. Are you sure?",
                    "glowfish", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes) {
                    Clients.Remove(c);
                    mainWindow.save();
                    ClientsListView.ItemsSource = null;
                    ClientsListView.ItemsSource = Clients;
                    ClientsListView.SelectedIndex = 0;
                    this.UpdateLayout();
                }
            }
        }

        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Client c = (Client)ClientsListView.SelectedItem;
            if(c != null) {
                firstNameTextBox.Text = c.FirstName;
                lastNameTextBox.Text  = c.LastName;
                numLevel1MinutesTextBox.Text = c.NumLevel1Minutes.ToString();
                numLevel2MinutesTextBox.Text = c.NumLevel2Minutes.ToString();
                level1Calendar.SelectedDate = c.Level1TanThroughDate;
                level2Calendar.SelectedDate = c.Level2TanThroughDate;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e) {
            Client c = (Client)ClientsListView.SelectedItem;
            int parsedLevel1Minutes;
            int parsedLevel2Minutes;
            try {
                parsedLevel1Minutes = int.Parse(numLevel1MinutesTextBox.Text);
                parsedLevel2Minutes = int.Parse(numLevel2MinutesTextBox.Text);
                if(parsedLevel1Minutes < 0 || parsedLevel2Minutes < 0) {
                    throw new Exception();
                }
            }
            catch(Exception) {
                MessageBox.Show("Only numeric values greater than or equal to zero are allowed for the number of minutes.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Clients.Remove(c);

            if(firstNameTextBox.Text.Length > 15 || lastNameTextBox.Text.Length > 15) {
                MessageBox.Show("Name too long. Names longer than 15 characters are not allowed.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else {
                c.FirstName = firstNameTextBox.Text;
                c.LastName = lastNameTextBox.Text;
                c.NumLevel1Minutes = parsedLevel1Minutes;
                c.NumLevel2Minutes = parsedLevel2Minutes;
                c.Level1TanThroughDate = (DateTime)level1Calendar.SelectedDate;
                c.Level2TanThroughDate = (DateTime)level2Calendar.SelectedDate;
                Clients.Insert(c);
                Clients.Sort();
                ClientsListView.ItemsSource = null;
                ClientsListView.ItemsSource = Clients;
                c.Log.Edited();
                this.UpdateLayout();

                mainWindow.save();
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            mainWindow.listView1.ItemsSource = null;
            mainWindow.listView1.ItemsSource = Clients;
            mainWindow.listView1.SelectedIndex = 0;
            mainWindow.UpdateLayout();
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(oldPasswordBox.Password == mainWindow.Password
                    && newPasswordBox1.Password == newPasswordBox2.Password) {
                mainWindow.Password = newPasswordBox1.Password;
                mainWindow.save();
                MessageBox.Show("Password Changed", "glowfish", MessageBoxButton.OK, MessageBoxImage.Information);
                oldPasswordBox.Clear();
                newPasswordBox1.Clear();
                newPasswordBox2.Clear();
                tabControl1.SelectedIndex = 0;
            }
            else if(oldPasswordBox.Password != mainWindow.Password
                    && newPasswordBox1.Password != newPasswordBox2.Password) {
                MessageBox.Show("Incorrect old Password. New Password fields do not match.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                oldPasswordBox.Clear();
                newPasswordBox1.Clear();
                newPasswordBox2.Clear();
                oldPasswordBox.Focus();
            }
            else if(oldPasswordBox.Password != mainWindow.Password
                    && newPasswordBox1.Password == newPasswordBox2.Password) {
                MessageBox.Show("Incorrect old Password.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                oldPasswordBox.Clear();
                oldPasswordBox.Focus();
            }
            else if(oldPasswordBox.Password == mainWindow.Password
                    && newPasswordBox1.Password != newPasswordBox2.Password) {
                MessageBox.Show("New Password fields do not match.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                newPasswordBox1.Clear();
                newPasswordBox2.Clear();
                newPasswordBox1.Focus();
            }
            else {
                MessageBox.Show("Error.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                oldPasswordBox.Clear();
                newPasswordBox1.Clear();
                newPasswordBox2.Clear();
                oldPasswordBox.Focus();
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            if(level1Calendar.IsKeyboardFocusWithin) {
                Mouse.Capture(saveButton);
                Keyboard.Focus(saveButton);
            }
            if(level2Calendar.IsKeyboardFocusWithin) {
                Mouse.Capture(saveButton);
                Keyboard.Focus(saveButton);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
