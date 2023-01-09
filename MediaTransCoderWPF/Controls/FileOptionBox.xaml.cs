using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string? InputPath {
            get { return inputPath; }
            set {
                inputPath = value;
                if(inputPath == null) {
                    inputFile.ToolTip = InputPath?.ToString();
                    inputDirectory.ToolTip = InputPath?.ToString();
                    inputDirectory.Background = Brushes.LightGray;
                    inputFile.Background = Brushes.LightGray;
                } else {
                    inputFile.ToolTip = InputPath?.ToString();
                    inputDirectory.ToolTip = InputPath?.ToString();
                    inputDirectory.Background = Brushes.LightGreen;
                    inputFile.Background = Brushes.LightGreen;
                }
            }
        }
        public string? OutputPath { 
            get { return outputPath; }
            set {
                outputPath = value;
                if(outputPath == null) {
                    outputDirectory.ToolTip = OutputPath?.ToString();
                    outputDirectory.Background = Brushes.LightGray;
                } else {
                    outputDirectory.ToolTip = OutputPath?.ToString();
                    outputDirectory.Background = Brushes.LightGreen;
                }
            }
        }
        private string? inputPath;
        private string? outputPath;

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
            bool isInputDir = false;
            if(InputPath != null) {
                var attr = File.GetAttributes(InputPath);
                if(attr.HasFlag(FileAttributes.Directory)) {
                    isInputDir = true;
                }
            }
            if (Input == InputOptions.FILE) {
                inputFile.Visibility = Visibility.Visible;
                inputDirectory.Visibility = Visibility.Hidden;
                if(isInputDir) {
                    InputPath = null;
                }
            } else {
                inputDirectory.Visibility = Visibility.Visible;
                inputFile.Visibility = Visibility.Hidden;
                if (!isInputDir) {
                    InputPath = null;
                }
            }
        }

        private void inputFile_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new OpenFileDialog()) {
                dialog.CheckFileExists = true;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    InputPath = dialog.FileName;
                } else {
                    InputPath = null;
                }
            }
        }

        private void inputDirectory_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    InputPath = dialog.SelectedPath;
                } else {
                    InputPath = null;
                }
            }
        }

        private void outputDirectory_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    OutputPath = dialog.SelectedPath;
                } else {
                    OutputPath = null;
                }
            }
        }
    }
}
