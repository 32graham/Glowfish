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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window {

        private MainWindow mainWin;

        public ChangePasswordDialog(MainWindow mainWin) {
            InitializeComponent();

            this.mainWin = mainWin; 
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(newPasswordBox.Password == confirmNewPasswordBox.Password && oldPasswordBox.Password == mainWin.Password) {
                mainWin.Password = newPasswordBox.Password;
                MessageBox.Show("Password changed.", "Glowfish", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else {
                MessageBox.Show("Password Error.", "Glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                oldPasswordBox.Clear();
                newPasswordBox.Clear();
                confirmNewPasswordBox.Clear();
                oldPasswordBox.Focus();
            }
        }
    }
}
