using System;
using System.Windows;
using System.Windows.Controls;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for NumberInput.xaml
    /// </summary>
    public partial class NumberInput : UserControl {
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
            }
        }
        public int MaxValue { get; set; } = 100;
        public int MinValue { get; set; } = 0;
        public int Value {
            get {
                return value;
            }
            set {
                if(value < MinValue || value > MaxValue) {
                    throw new ArgumentOutOfRangeException();
                }
                this.value = value;
                valueBox.Text = value.ToString();
            }
        }
        private int value = 0;

        public NumberInput() {
            InitializeComponent();
            Length = 50;
        }

        private void minusButton_Click(object sender, RoutedEventArgs e) {
            if(value > MinValue) {
                Value--;
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e) {
            if(value < MaxValue) {
                Value++;
            }
        }
    }
}
