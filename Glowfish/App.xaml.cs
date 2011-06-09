using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Glowfish {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public int myAppInt = 5;

        public App() {

            this.Resources["Light"] = Colors.Azure;
            this.Resources["Dark"] = Color.FromArgb(255, 38, 38, 38);

        }

        public void ChangeColorScheme(Color darkColor, Color lightColor) {
            this.Resources["Light"] = lightColor;
            this.Resources["Dark"] = darkColor;
        }

    }
}
