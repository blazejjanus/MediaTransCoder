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
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl {
        public string WindowTitle { get; private set; }
        private readonly MainWindow window;

        public MenuView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            WindowTitle = "MediaTransCoder";
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e) {
            window.SetSettingsView();
        }

        private void imageButton_Click(object sender, RoutedEventArgs e) {
            window.SetImageView();
        }

        private void videoButton_Click(object sender, RoutedEventArgs e) {
            window.SetVideoView();
        }

        private void audioButton_Click(object sender, RoutedEventArgs e) {
            window.SetAudioView();
        }
    }
}
