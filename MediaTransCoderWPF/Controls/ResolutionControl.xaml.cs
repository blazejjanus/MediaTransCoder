using System;
using System.Numerics;
using System.Windows.Controls;

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
                resX.Value = (int)resolution.X;
                resY.Value = (int)resolution.Y;
                UpdateProportion();
            }
        }
        private Vector2 resolution;
        public ResolutionControl() {
            InitializeComponent();
            resX.MinValue = 0;
            resX.MaxValue = 8192;
            resX.Multiplayer = 10;
            resY.MinValue = 0;
            resY.MaxValue = 4320;
            resY.Multiplayer = 10;
            Resolution = new Vector2(1920, 1080);
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

        private void resX_ValueChanged(object sender, EventArgs e) {
            resolution = new Vector2(resX.Value, resolution.Y);
            UpdateProportion();
        }

        private void resY_ValueChanged(object sender, EventArgs e) {
            resolution = new Vector2(resolution.X, resY.Value);
            UpdateProportion();
        }
    }
}
