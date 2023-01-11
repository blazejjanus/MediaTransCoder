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
    /// Interaction logic for PercentBar.xaml
    /// </summary>
    public partial class PercentBar : UserControl {
        public string Description {
            get {
                return barText.Text;
            }
            set {
                barText.Text = value;
            }
        }
        public int? Value {
            get {
                if(barInput.Value != 0) {
                    return (int)barInput.Value;
                } else {
                    return null;
                }
            }
            set {
                if(value < -100 || value > 100) {
                    throw new ArgumentOutOfRangeException();
                }
                if(value != null) {
                    barInput.Value = value.Value;
                } else {
                    barInput.Value = 0;
                }
                barValue.Text = value.ToString();
            }
        }
        public PercentBar() {
            InitializeComponent();
            Value = 0;
        }

        private void barInput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Value = (int)barInput.Value;
        }

        private void barMinLabel_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Value = -100;
        }

        private void barZeroLabel_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Value = 0;
        }

        private void barMaxLabel_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Value = 100;
        }
    }
}
