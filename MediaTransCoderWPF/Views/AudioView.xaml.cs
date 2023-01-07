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
    /// Interaction logic for AudioView.xaml
    /// </summary>
    public partial class AudioView : UserControl {
        public string WindowTitle { get; private set; }
        private readonly MainWindow window;
        public AudioView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            WindowTitle = "MediaTransCoder - Audio";
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            window.SetMenuView();
        }
    }
}
