using System;
using System.Collections.Generic;
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

namespace MediaTransCoder.WPF.Views {
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl {
        public string WindowTitle { get; private set; }
        private readonly MainWindow window;

        public SettingsView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            if(window.Context.Config == null) {
                throw new Exception("Provided config was null!");
            }
            WindowTitle = "MediaTransCoder - Settings";
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            window.SetMenuView();
        }
    }
}
