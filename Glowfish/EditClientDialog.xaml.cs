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
    /// Interaction logic for EditClientDialog.xaml
    /// </summary>
    public partial class EditClientDialog : Window {

        private MainWindow main;

        public EditClientDialog(MainWindow mainWindow) {
            InitializeComponent();
            Owner = mainWindow;
            main = mainWindow;

            populateFields();
        }

        private void populateFields() {
            string name = main.listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);

            firstNameTextBox.Text = c.FirstName;
            lastNameTextBox.Text = c.LastName;

            level1MinutesTextBox.Text = c.NumLevel1Minutes.ToString();
            level2MinutesTextBox.Text = c.NumLevel2Minutes.ToString();

            level1TanThroughDateCalendar.SelectedDate = c.Level1TanThroughDate;
            level2TanThroughDateCalendar.SelectedDate = c.Level2TanThroughDate;
        }

        private bool applyChanges() {
            bool success = true;

            string name = main.listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);

            int lv1Minutes = int.Parse(level1MinutesTextBox.Text);
            int lv2Minutes = int.Parse(level2MinutesTextBox.Text);

            if(lv1Minutes < 0 || lv2Minutes < 0) {
                main.AnimateTextBox(level1MinutesTextBox, Colors.Red);
                main.AnimateTextBox(level2MinutesTextBox, Colors.Red);
                MessageBox.Show("The number of minutes must be zero or more.", "Glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            else {
                c.FirstName = firstNameTextBox.Text;
                c.LastName = lastNameTextBox.Text;

                c.NumLevel1Minutes = lv1Minutes;
                c.NumLevel2Minutes = lv2Minutes;

                c.Level1TanThroughDate = (DateTime)level1TanThroughDateCalendar.SelectedDate;
                c.Level2TanThroughDate = (DateTime)level2TanThroughDateCalendar.SelectedDate;

                DataManager.UpdateClient(c);

            }
            return success;

        }

        private void applyButton_Click(object sender, RoutedEventArgs e) {
            applyChanges();
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(applyChanges()) {
                this.Close();
            }
        }

        private void level1TanThroughDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            releaseCalendar();
        }

        private void level2TanThroughDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            releaseCalendar();
        }

        private void releaseCalendar() {
            if(level1TanThroughDateCalendar.IsKeyboardFocusWithin || level2TanThroughDateCalendar.IsKeyboardFocusWithin) {
                Mouse.Capture(okButton);
                Keyboard.Focus(okButton);
            }
        }
    }
}
