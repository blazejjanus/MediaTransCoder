using MediaTransCoder.Backend;
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

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for ImageOptionBox.xaml
    /// </summary>
    public partial class ImageOptionBox : UserControl {
        public ImageOptions Image { get; set; }
        public ImageOptionBox() {
            InitializeComponent();
            Image = new ImageOptions();
        }

        private void PreFillForm() {

        }
    }
}
