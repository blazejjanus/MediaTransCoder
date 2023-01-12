using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static System.Windows.Forms.DataFormats;

namespace MediaTransCoder.WPF.Views {
    /// <summary>
    /// Interaction logic for AudioView.xaml
    /// </summary>
    public partial class AudioView : UserControl, IRefreshableControl {
        public string WindowTitle { get; private set; }
        public ContainerFormat Format {
            get {
                return format;
            }
            set {
                format = value;
                audioBox.SelectedFormat = format;
                audioBox.Refresh();
            }
        }
        private ContainerFormat format;
        private readonly MainWindow window;

        public AudioView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            WindowTitle = "MediaTransCoder - Audio";
            presetBox.ParentView = this;
            window.Context.Display.Target = DisplayOutput;
            PreFillForm();
        }

        public void DisplayOutput(string message) {
            this.Dispatcher.Invoke(() => {
                resultText.Text += "\n" + message;
            });
        }

        public void Refresh() {
            if (presetBox.UsePreset == true) {
                formatInput.IsEnabled = false;
                audioBox.IsEnabled = false;
                Preset? preset = presetBox.GetPreset();
                if (preset != null) {
                    if (preset.Options.Format != null) {
                        this.Format = preset.Options.Format.Value;
                    }
                    if (preset.Options.Audio != null) {
                        audioBox.Audio = preset.Options.Audio;
                    }
                }
            } else {
                formatInput.IsEnabled = true;
                audioBox.IsEnabled = true;
            }
        }

        private void formatInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<ContainerFormat> formats = EnumHelper.GetVideoFormats();
            foreach (ContainerFormat format in formats) {
                if (EnumHelper.GetName(format) == formatInput.SelectedItem.ToString()) {
                    this.Format = format;
                    break;
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            PreFillForm();
            window.SetMenuView();
        }

        public void PreFillForm() {
            resultsScroll.Visibility = Visibility.Hidden;
            resultText.Text = "";
            if (presetBox.UsePreset == true) {
                formatInput.IsEnabled = false;
                audioBox.IsEnabled = false;
            } else {
                formatInput.IsEnabled = true;
                audioBox.IsEnabled = true;
            }
            Format = ContainerFormat.matroska;
            List<ContainerFormat> formats = EnumHelper.GetVideoFormats();
            foreach (ContainerFormat format in formats) {
                formatInput.Items.Add(EnumHelper.GetName(format));
            }
            if (formatInput.Items.Contains(EnumHelper.GetName(format))) {
                formatInput.SelectedIndex = formatInput.Items.IndexOf(EnumHelper.GetName(format));
            }
        }

        private bool Validate() {
            if (!inputBox.IsValid) {
                window.Context.Display?.Send("Nieprawidłowe ustawienia plików źródłowych i docelowych!", MessageType.ERROR);
                return false;
            }
            if (audioBox.Audio == null) {
                window.Context.Display?.Send("Nieprawidłowe ustawienia audio!", MessageType.ERROR);
                return false;
            }
            return true;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e) {
            if (Validate()) {
                window.Context.Display.RedirectOutput = true;
                EndpointOptions args = new EndpointOptions();
                args.InputOption = inputBox.Input.Value;
                args.Input = inputBox.InputPath;
                args.Output = inputBox.OutputPath;
                args.OverrideExistingFiles = true;
                args.SkipExistingFiles = false;
                args.AudioOnly = true;
                args.AllowDirectoryCreation = true;
                args.Acceleration = window.Context.DefaultAcceleration;
                args.Format = format;
                args.Audio = audioBox.Audio;
                args.Video = null;
                args.Image = null;
                backButton.IsEnabled = false;
                resultsScroll.Visibility = Visibility.Visible;
                if (window.Context.Backend == null) {
                    throw new Exception("Nie można wywołać backendu!");
                }
                Thread thread = new Thread(() => window.Context.Backend.ConvertAudio(args));
                thread.Start();
                backButton.IsEnabled = true;
                window.Context.Display.RedirectOutput = false;
            }
        }
    }
}
