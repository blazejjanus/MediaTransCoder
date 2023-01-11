using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for ResolutionControl.xaml
    /// </summary>
    public partial class ResolutionControl : UserControl {
        public Vector2 Resolution {
            get {
                return resolution;
            }
            set {
                if (value.X < 0 || value.Y < 0) {
                    throw new ArgumentOutOfRangeException();
                }
                resolution = value;
                UpdateProportion();
            }
        }
        private Vector2 resolution;
        public ResolutionControl() {
            InitializeComponent();
            Resolution = new Vector2(1920, 1080);
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UpdateProportion() {
            double ratio = Resolution.X / Resolution.Y;
            double value = Math.Floor(ratio);
            double delta = ratio - value;
            long precision = 1000000000;
            long divider = GetGreatestDivider((long)Math.Round(delta * precision), precision);
            long numerator = (long)Math.Round(delta * precision) / divider;
            long denominator = precision / divider;
            propValue.Text = numerator + ":" + denominator;
        }

        private long GetGreatestDivider(long val1, long val2) {
            if(val1 == 0) {
                return val2;
            }
            if(val2 == 0) {
                return val1;
            }
            if (val1 < val2) {
                return GetGreatestDivider(val1, val2 % val1);
            } else {
                return GetGreatestDivider(val2, val1 % val2);
            }
        }

        private void resX_TextChanged(object sender, TextChangedEventArgs e) {
            int x = int.Parse(resX.Text.Trim());
            int y = (int)resolution.Y;
            Resolution = new Vector2(x, y);
        }

        private void resY_TextChanged(object sender, TextChangedEventArgs e) {
            int x= (int)resolution.X;
            int y = int.Parse(resY.Text.Trim());
            Resolution = new Vector2(x, y);
        }
    }
}
