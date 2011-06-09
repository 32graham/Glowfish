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
using System.Windows.Shapes;

namespace Glowfish {
    /// <summary>
    /// Interaction logic for AddClientDialog.xaml
    /// </summary>
    public partial class AddClientDialog : Window {

        private MainWindow main;

        public AddClientDialog(MainWindow mainWindow) {
            InitializeComponent();
            Owner = mainWindow;
            main = mainWindow;
            firstNameTextBox.Focus();
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            string fName = firstNameTextBox.Text.ToString();
            string lName = lastNameTextBox.Text.ToString();
            
            Client newClient = new Client(fName, lName);

            DataManager.AddClient(newClient);
            main.listView1.ItemsSource = DataManager.NameList;
            main.listView1.SelectedItem = newClient.ToString();

            main.UpdateClientInfo();

            this.Close();
        }
    }
}
