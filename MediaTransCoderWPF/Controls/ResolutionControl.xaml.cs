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
                resX.Text = resolution.X.ToString();
                resY.Text = resolution.Y.ToString();
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
            int divider = GetGreatestDivider((int)Resolution.X, (int)Resolution.Y);
            string ratio = string.Empty;
            if (divider != 0) {
                double xRatio, yRatio;
                xRatio = Resolution.X / divider;
                yRatio = Resolution.Y / divider;
                ratio = Math.Round(xRatio, 2) + ":" + Math.Round(yRatio, 2);
            }
            propValue.Text = ratio;
        }

        private int GetGreatestDivider(int val1, int val2) {
            while (val1 != 0 && val2 != 0) {
                if (val1 > val2)
                    val1 %= val2;
                else
                    val2 %= val1;
            }
            return val1 | val2;
        }

        private void resX_TextChanged(object sender, TextChangedEventArgs e) {
            if(resX.Text.Length > 0) {
                int x = int.Parse(resX.Text.Trim());
                int y = (int)resolution.Y;
                Resolution = new Vector2(x, y);
            }
        }

        private void resY_TextChanged(object sender, TextChangedEventArgs e) {
            if(resY.Text.Length > 0) {
                int x = (int)resolution.X;
                int y = int.Parse(resY.Text.Trim());
                Resolution = new Vector2(x, y);
            }
        }
    }
}
