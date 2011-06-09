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
    /// Interaction logic for AddTimeDialog.xaml
    /// </summary>
    public partial class AddTimeDialog : Window {

        private MainWindow main;

        private int selectedTime;// = "100 Minutes";
        private int    selectedLevel;// = 1;

        public AddTimeDialog(MainWindow mainWindow) {
            InitializeComponent();
            Owner = mainWindow;
            main = mainWindow;
        }

        private void addTimeButton_Click(object sender, RoutedEventArgs e) {
            string name = main.listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);
            if(c != null) {
                if(selectedLevel == 1) {
                    if (selectedTime > 0) {
                        c.NumLevel1Minutes += selectedTime;
                        main.AnimateLabel(main.level1MinutesLabel, Colors.LightGreen);
                    } else if(selectedTime == -1) {
                        c.Level1TanThroughDate = DateTime.Today.AddDays(7);
                        main.AnimateLabel(main.level1TanThroughDateLabel, Colors.LightGreen);
                    }
                    else if(selectedTime == -2) {
                        c.Level1TanThroughDate = DateTime.Today.AddMonths(1);
                        main.AnimateLabel(main.level1TanThroughDateLabel, Colors.LightGreen);
                    }
                }
                else if(selectedLevel == 2) {
                    if(selectedTime > 0) {
                        c.NumLevel2Minutes += selectedTime;
                        main.AnimateLabel(main.level2MinutesLabel, Colors.LightGreen);
                    }
                    else if(selectedTime == -1) {
                        c.Level2TanThroughDate = DateTime.Today.AddDays(7);
                        main.AnimateLabel(main.level2TanThroughDateLabel, Colors.LightGreen);
                    }
                    else if(selectedTime == -2) {
                        c.Level2TanThroughDate = DateTime.Today.AddMonths(1);
                        main.AnimateLabel(main.level2TanThroughDateLabel, Colors.LightGreen);
                    }
                }
            }
            else {
                MessageBox.Show("error");
            }
            DataManager.UpdateClient(c);
            main.UpdateClientInfo();
            this.Close();
        }

        private void TimeRadioButton_Checked(object sender, RoutedEventArgs e) {
            RadioButton b = sender as RadioButton;

            selectedTime = int.Parse(b.Tag.ToString());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {
            RadioButton b = sender as RadioButton;
            if(b.Content.ToString() == "Level _One") {
                selectedLevel = 1;
            }
            else if(b.Content.ToString() == "Level _Two") {
                selectedLevel = 2;
            }
        }
    }
}
