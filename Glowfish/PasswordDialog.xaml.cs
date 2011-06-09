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
    /// Interaction logic for passwordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window {
        
        MainWindow mainWindow;

        public PasswordDialog(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
            passwordBox.Focus();
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(passwordBox.Password == mainWindow.Password) {
                this.DialogResult = true;
                this.Close();
            }
            else {
                MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Clear();
                passwordBox.Focus();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
