using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for NumberInput.xaml
    /// </summary>
    public partial class NumberInput : UserControl {
        /// <summary>
        /// ValueChanged event
        /// </summary>
        /// <value>Is raised when the value of the NumberInput changes</value>
        public event EventHandler? ValueChanged;
        /// <summary>
        /// Value box Width
        /// </summary>
        /// <value>Value box Width</value>
        public int Length {
            get {
                return (int)valueBox.Width;
            }
            set {
                if(value < 50) {
                    throw new ArgumentOutOfRangeException();
                }
                valueBox.Width = value;
                minusButton.Margin = new Thickness(value, 0, 0, 0);
                plusButton.Margin = new Thickness(value + 25, 0, 0, 0);
            }
        }
        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }
        public int Increment { get; set; } = 1;
        public int Multiplayer { get; set; } = 1;
        public int Value {
            get {
                return value;
            }
            set {
                if(MinValue != null) {
                    if(value < MinValue) {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                if(MaxValue != null) {
                    if(value > MaxValue) {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                this.value = value;
                if(ValueChanged != null) {
                    ValueChanged(this, EventArgs.Empty);
                }
                valueBox.Text = value.ToString();
            }
        }
        private int value = 0;

        public NumberInput() {
            InitializeComponent();
            Length = 50;
            if(Multiplayer > 1) {
                plusButton.ToolTip = "LCtrl - x" + Multiplayer;
                minusButton.ToolTip = "LCtrl - x" + Multiplayer;
            }
        }

        private void minusButton_Click(object sender, RoutedEventArgs e) {
            int increment = Increment;
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                increment *= Multiplayer;
            }
            if(Value >= MinValue + increment) {
                Value -= increment;
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e) {
            int increment = Increment;
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                increment *= Multiplayer;
            }
            if (value <= MaxValue - increment) {
                Value += increment;
            }
        }
    }
}
