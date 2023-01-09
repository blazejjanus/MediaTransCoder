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
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using UserControl = System.Windows.Controls.UserControl;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for FileOptionBox.xaml
    /// </summary>
    public partial class FileOptionBox : UserControl {
        public InputOptions? Input { get; set; }
        public string? InputPath { get; set; }
        public string? OutputPath { get; set; }
        public FileOptionBox() {
            InitializeComponent();
            PrefillForm();
        }

        private void PrefillForm() {
            var inputOptions = Enum.GetValues(typeof(InputOptions));
            foreach(var inputOption in inputOptions) {
                inputTypeComboBox.Items.Add(inputOption.ToString());
            }
            if (inputTypeComboBox.Items.Contains(InputOptions.FILE.ToString())) {
                inputTypeComboBox.SelectedIndex = inputTypeComboBox.Items.IndexOf(InputOptions.FILE.ToString());
                Input = InputOptions.FILE;
            }
            if(inputTypeComboBox.SelectedItem.ToString() == InputOptions.FILE.ToString()) {
                inputFile.Visibility = Visibility.Visible;
                inputDirectory.Visibility = Visibility.Hidden;
            } else {
                inputDirectory.Visibility = Visibility.Visible;
                inputFile.Visibility = Visibility.Hidden;
            }
        }

        private void inputTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var inputOptions = Enum.GetValues(typeof(InputOptions));
            foreach (InputOptions inputOption in inputOptions) {
                if (inputTypeComboBox.SelectedItem.ToString() == inputOption.ToString()) {
                    Input = inputOption;
                    break;
                }
            }
            if(Input == InputOptions.FILE) {
                inputFile.Visibility = Visibility.Visible;
                inputDirectory.Visibility = Visibility.Hidden;
            } else {
                inputDirectory.Visibility = Visibility.Visible;
                inputFile.Visibility = Visibility.Hidden;
            }
        }

        private void inputFile_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new OpenFileDialog()) {
                dialog.CheckFileExists = true;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    InputPath = dialog.FileName;
                    inputFile.ToolTip = InputPath.ToString();
                    inputDirectory.ToolTip = InputPath.ToString();
                }
            }
        }

        private void inputDirectory_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    InputPath = dialog.SelectedPath;
                    inputFile.ToolTip = InputPath.ToString();
                    inputDirectory.ToolTip = InputPath.ToString();
                }
            }
        }

        private void outputDirectory_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    OutputPath = dialog.SelectedPath;
                    outputDirectory.ToolTip = OutputPath.ToString();
                }
            }
        }
    }
}
