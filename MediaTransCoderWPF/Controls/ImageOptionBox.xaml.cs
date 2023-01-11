using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for ImageOptionBox.xaml
    /// </summary>
    public partial class ImageOptionBox : UserControl, IRefreshableControl {
        public ImageOptions Image { get; set; }
        public ImageOptionBox() {
            InitializeComponent();
            Image = new ImageOptions();
        }

        public void Refresh() {
            throw new NotImplementedException();
        }

        private void PreFillForm() {

        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void formatInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void pxfInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void jcrInput_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
