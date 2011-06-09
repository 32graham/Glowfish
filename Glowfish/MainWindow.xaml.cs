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
using System.Threading;
using System.Windows.Media.Animation;

namespace Glowfish {
    public partial class MainWindow : Window {

        internal string Password;
        private int key;
        private string selectedTime = "100";
        private int    selectedLevel = 1;
        public List<string> things;

        public MainWindow() {

            InitializeComponent();
            slider1.Value = 10;
            key = 42;

            Password = "tan";

            DataManager.TryCreateTable();
                
            listView1.ItemsSource = DataManager.NameList;

        }

        internal void predictAndSetLevelRadioButtons() {
            string name = listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);

            if(c.HasLevel2Unlimited) {
                level2RadioButton.IsChecked = true;
            }
            else if(c.HasLevel1Unlimited) {
                level1RadioButton.IsChecked = true;
            }
            else if(c.HasLevel2Time) {
                level2RadioButton.IsChecked = true;
            }
            else {
                level1RadioButton.IsChecked = true;
            }
        }

        internal void AnimateLabel(Label label, Color color) {
            // Attaching the NameScope to the label makes sense if you're only animating
            // things that belong to that label; this allows you to animate any number
            // of labels simultaneously with this method without SetTargetName setting
            // the wrong thing as the target.
            NameScope.SetNameScope(label, new NameScope());
            label.Background = new SolidColorBrush(color);
            label.RegisterName("Brush", label.Background);

            ColorAnimation highlightAnimation = new ColorAnimation();
            highlightAnimation.To = Colors.Transparent;
            highlightAnimation.Duration = TimeSpan.FromSeconds(3);

            Storyboard.SetTargetName(highlightAnimation, "Brush");
            Storyboard.SetTargetProperty(highlightAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard sb = new Storyboard();
            sb.Children.Add(highlightAnimation);
            sb.Begin(label);
        }

        internal void AnimateTextBox(TextBox textBox, Color color) {
            // Attaching the NameScope to the label makes sense if you're only animating
            // things that belong to that label; this allows you to animate any number
            // of labels simultaneously with this method without SetTargetName setting
            // the wrong thing as the target.
            NameScope.SetNameScope(textBox, new NameScope());
            textBox.Background = new SolidColorBrush(color);
            textBox.RegisterName("Brush", textBox.Background);

            ColorAnimation highlightAnimation = new ColorAnimation();
            highlightAnimation.To = Colors.Transparent;
            highlightAnimation.Duration = TimeSpan.FromSeconds(3);

            Storyboard.SetTargetName(highlightAnimation, "Brush");
            Storyboard.SetTargetProperty(highlightAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard sb = new Storyboard();
            sb.Children.Add(highlightAnimation);
            sb.Begin(textBox);
        }

        internal void UpdateClientInfo() {

            string name = listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);

            level1MinutesLabel.Content = "";
            level2MinutesLabel.Content = "";
            level1TanThroughDateLabel.Content = "";
            level2TanThroughDateLabel.Content = "";

            level1MinutesLabel.Content = c.NumLevel1Minutes;
            level2MinutesLabel.Content = c.NumLevel2Minutes;

            if(c.HasLevel1Unlimited) {
                level1TanThroughDateLabel.Content = c.Level1TanThroughDate.ToShortDateString();
            }
            else {
                level1TanThroughDateLabel.Content = "No";
            }
            if(c.HasLevel2Unlimited) {
                level2TanThroughDateLabel.Content = c.Level2TanThroughDate.ToShortDateString();
            }
            else {
                level2TanThroughDateLabel.Content = "No";
            }


            logBox.Text = c.History.ToScreen();
        }



        private string EncryptDecrypt(string textToEncrypt) {
            StringBuilder inSb = new StringBuilder(textToEncrypt);
            StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
            char c;
            for(int i = 0; i < textToEncrypt.Length; i++) {
                c = inSb[i];
                c = (char)(c ^ key);
                outSb.Append(c);
            }
            return outSb.ToString();
        }   

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            
            if(listView1.SelectedItem != null) {
                string name = listView1.SelectedItem as string;
                Client c = DataManager.GetClient(name);
                predictAndSetLevelRadioButtons();
                UpdateClientInfo();
            }
            
        }
        
        private void tanButton_Click(object sender, RoutedEventArgs e) {

            string name = listView1.SelectedItem as string;
            Client c = DataManager.GetClient(name);
            
            try {
                int value = int.Parse(textBox1.Text);
                if(value != slider1.Value) {
                    throw new Exception();
                }
            }
            catch(Exception) {
                MessageBox.Show("Only numeric values greater than zero are allowed for the number of minutes.", "glowfish", MessageBoxButton.OK, MessageBoxImage.Warning);
                textBox1.Clear();
                return;
            }


            if((bool)level1RadioButton.IsChecked) {
                if(c.Level1TanThroughDate >= DateTime.Today) {
                    c.History.Tanned((int)slider1.Value, 1);
                    AnimateLabel(level1TanThroughDateLabel, Colors.LightSkyBlue);
                    UpdateClientInfo();
                }
                else if(c.NumLevel1Minutes - (int)slider1.Value >= 0) {
                    c.NumLevel1Minutes -= (int)slider1.Value;
                    c.History.Tanned((int)slider1.Value, 1);
                    AnimateLabel(level1MinutesLabel, Colors.LightSkyBlue);
                    UpdateClientInfo();
                }
                else {
                    AnimateLabel(level1TanThroughDateLabel, Colors.Red);
                    AnimateLabel(level1MinutesLabel, Colors.Red);
                    MessageBox.Show("Not enough minutes", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if((bool)level2RadioButton.IsChecked) {
                if(c.Level2TanThroughDate >= DateTime.Today) {
                    c.History.Tanned((int)slider1.Value, 2);
                    AnimateLabel(level2TanThroughDateLabel, Colors.LightSkyBlue);
                    UpdateClientInfo();
                }
                else if(c.NumLevel2Minutes - (int)slider1.Value >= 0) {
                    c.NumLevel2Minutes -= (int)slider1.Value;
                    c.History.Tanned((int)slider1.Value, 2);
                    AnimateLabel(level2MinutesLabel, Colors.LightSkyBlue);
                    UpdateClientInfo();
                }
                else {
                    AnimateLabel(level2TanThroughDateLabel, Colors.Red);
                    AnimateLabel(level2MinutesLabel, Colors.Red);
                    MessageBox.Show("Not enough minutes", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("Please select a level.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            DataManager.UpdateClient(c);
            UpdateClientInfo();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            selectedTime = button.Content.ToString();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) {
            RadioButton button = sender as RadioButton;

            if(button.Content.ToString() == "Level 1") {
                selectedLevel = 1;
            }
            else if(button.Content.ToString() == "Level 2") {
                selectedLevel = 2;
            }
            else {
                MessageBox.Show("Error reading level", "glowfish", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addTimeButton_Click(object sender, RoutedEventArgs e) {
            PasswordDialog passwordWindow = new PasswordDialog(this);
            if((bool)passwordWindow.ShowDialog()) {
                Window addTimeDialog = new AddTimeDialog(this);
                addTimeDialog.ShowDialog();
                predictAndSetLevelRadioButtons();
            }
        }

        private void addClientButton_Click(object sender, RoutedEventArgs e) {
            AddClientDialog addClientWindow = new AddClientDialog(this);
            addClientWindow.ShowDialog();
        }

        private void removeClientButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("This will permanantly delete the client and all associated data.\n\nContinue?",
                                        "Glowfish", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) {
                PasswordDialog passwordWindow = new PasswordDialog(this);
                if((bool)passwordWindow.ShowDialog()) {
                    
                    string name = listView1.SelectedItem as string;
                    
                    listView1.SelectedIndex = 0;

                    DataManager.RemoveClient(name);

                    listView1.ItemsSource = DataManager.NameList;


                }
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e) {
            PasswordDialog passwordWindow = new PasswordDialog(this);
            if((bool)passwordWindow.ShowDialog()) {
                EditClientDialog editClientWindow = new EditClientDialog(this);
                editClientWindow.ShowDialog();
            }
            UpdateClientInfo();
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e) {
            AboutWindow aboutWin = new AboutWindow();
            aboutWin.ShowDialog();
        }

        private void changeColorAzure_Click(object sender, RoutedEventArgs e) {
            MenuItem m = sender as MenuItem;
            
            clearColorChecks();

            ((App)Application.Current).ChangeColorScheme(Color.FromArgb(255, 38, 38, 38), Colors.Azure);

            m.IsChecked = true;
        }

        private void changeColorLavendar_Click(object sender, RoutedEventArgs e) {
            MenuItem m = sender as MenuItem;

            clearColorChecks();

            ((App)Application.Current).ChangeColorScheme(Color.FromArgb(255, 38, 38, 38), Colors.Lavender);

            m.IsChecked = true;
        }

        private void changeColorAlmond_Click(object sender, RoutedEventArgs e) {
            MenuItem m = sender as MenuItem;

            clearColorChecks();

            ((App)Application.Current).ChangeColorScheme(Color.FromArgb(255, 65, 50, 50), Colors.BlanchedAlmond);

            m.IsChecked = true;
        }

        private void clearColorChecks() {
            azureMenuItem.IsChecked = false;
            violetMenuItem.IsChecked = false;
            almondMenuItem.IsChecked = false;
        }

        private void editPassword_Click(object sender, RoutedEventArgs e) {
            ChangePasswordDialog cpd = new ChangePasswordDialog(this);
            cpd.ShowDialog();
        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

    }
}
