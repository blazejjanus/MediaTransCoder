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
using MediaTransCoder.Backend;

namespace MediaTransCoder.WPF.Views {
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl {
        public string WindowTitle { get; private set; }
        private readonly MainWindow window;

        public SettingsView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            if(window.Context.Config == null) {
                throw new Exception("Provided config was null!");
            }
            WindowTitle = "MediaTransCoder - Settings";
            PreFillForm();
        }

        private void PreFillForm() {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            //Detected hardware
            detectedCPUCoresValue.Text = window.Context.Config.Backend.Hardware.MaxCPUCores.ToString();
            List<GPUType> gpus = Enum.GetValues(typeof(GPUType)).Cast<GPUType>().ToList();
            foreach (GPUType cgpu in gpus) {
                detectedGPUValue.Items.Add(cgpu.ToString());
            }
            var gpu = window.Context.Config.Backend.Hardware.GPU;
            if (detectedGPUValue.Items.Contains(gpu.ToString())) {
                detectedGPUValue.SelectedIndex = detectedGPUValue.Items.IndexOf(gpu.ToString());
            }
            //Ffmpeg configuration
            string? path = window.Context.Config.Backend.FfmpegPath;
            if (path == null) {
                ffmpegPathValue.Text = "";
                ffmpegPathStatus.Text = "Błąd!";
                ffmpegDownloadButton.Visibility = Visibility.Visible;
            } else {
                ffmpegPathValue.Text = path;
                ffmpegPathStatus.Text = "OK";
                ffmpegDownloadButton.Visibility = Visibility.Hidden;
            }
            List<HardwareAcceleration> accelList = Enum.GetValues(typeof(HardwareAcceleration)).Cast<HardwareAcceleration>().ToList();
            foreach (HardwareAcceleration accel in accelList) {
                hwaccelComboBox.Items.Add(accel.ToString());
            }
            if (hwaccelComboBox.Items.Contains(HardwareAcceleration.CPU.ToString())) {
                hwaccelComboBox.SelectedIndex = hwaccelComboBox.Items.IndexOf(HardwareAcceleration.CPU.ToString());
                hwaccelDetailsCPU.Text = window.Context.Config.Backend.Hardware.CPUCores.ToString();
                hwaccelDetailsCPU.Visibility = Visibility.Visible;
                hwaccelDetailsText.Text = "Używana ilość wątków: ";
                hwaccelDetailsText.Visibility = Visibility.Visible;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            window.Context.Config.SaveConfig();
            window.SetMenuView();
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void detectedCPUCoresValue_TextChanged(object sender, TextChangedEventArgs e) {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            string input = detectedCPUCoresValue.Text.Trim();
            int cores = 0;
            if (int.TryParse(input, out cores)) {
                if (cores > 0) {
                    window.Context.Config.Backend.Hardware.MaxCPUCores = cores;
                    if (window.Context.Config.Backend.Hardware.CPUCores > window.Context.Config.Backend.Hardware.MaxCPUCores) {
                        window.Context.Config.Backend.Hardware.CPUCores = cores;
                        hwaccelDetailsCPU.Text = cores.ToString();
                    }
                } else {
                    window.Context.Display?.Send("Liczba nieujemna!", MessageType.ERROR);
                }
            }
            hwaccelDetailsCPU.Text = window.Context.Config?.Backend.Hardware.MaxCPUCores.ToString();
        }

        private void ffmpegPathValue_TextChanged(object sender, TextChangedEventArgs e) {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            TextBox textBox = sender as TextBox;
            if (window.Context.Backend?.CheckFfmpegPath(textBox.Text) ?? false) {
                window.Context.Config.Backend.FfmpegPath = textBox.Text;
                e.Handled = true;
            } else {
                e.Handled = false;
            }
        }

        private void hwaccelDetailsCPU_TextChanged(object sender, TextChangedEventArgs e) {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            string input = hwaccelDetailsCPU.Text.Trim();
            int cores = 0;
            if (int.TryParse(input, out cores)) {
                if (cores > 0) {
                    if (cores > window.Context.Config?.Backend.Hardware.MaxCPUCores) {
                        window.Context.Display?.Send("Nie można ustawić większej liczby rdzeni niż dostępna w systemie!", MessageType.ERROR);
                    } else {
                        if (window.Context.Config.Backend.Hardware.CPUCores > window.Context.Config.Backend.Hardware.MaxCPUCores) {
                            window.Context.Config.Backend.Hardware.CPUCores = cores;
                            hwaccelDetailsCPU.Text = cores.ToString();
                        }
                    }
                } else {
                    window.Context.Display?.Send("Liczba nieujemna!", MessageType.ERROR);
                }
            }
            hwaccelDetailsCPU.Text = window.Context.Config.Backend.Hardware.CPUCores.ToString();
        }

        private void detectedGPUValue_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (window.Context.Config == null) {
                throw new NullReferenceException();
            }
            List<GPUType> gpus = Enum.GetValues(typeof(GPUType)).Cast<GPUType>().ToList();
            foreach (GPUType gpu in gpus) {
                if(detectedGPUValue.SelectedItem.ToString() == gpu.ToString()) {
                    window.Context.Config.Backend.Hardware.GPU = gpu;
                    break;
                }
            }
        }

        private void hwaccelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(hwaccelComboBox.SelectedItem.ToString() == HardwareAcceleration.CPU.ToString()) {
                hwaccelDetailsText.Visibility = Visibility.Visible;
                hwaccelDetailsCPU.Visibility = Visibility.Visible;
            } else {
                hwaccelDetailsText.Visibility = Visibility.Hidden;
                hwaccelDetailsCPU.Visibility = Visibility.Hidden;
            }
        }
    }
}
