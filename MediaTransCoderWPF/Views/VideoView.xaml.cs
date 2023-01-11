using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MediaTransCoder.Backend;

namespace MediaTransCoder.WPF.Views {
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl, IRefreshableControl {
        public string WindowTitle { get; private set; }
        public ContainerFormat Format {
            get {
                return format;
            }
            set {
                format = value;
                audioBox.SelectedFormat = format;
                audioBox.Refresh();
                videoBox.SelectedFormat = format;
                videoBox.Refresh();
            }
        }
        private ContainerFormat format;
        private readonly MainWindow window;
        private WPFContext context = WPFContext.Get();

        public VideoView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            WindowTitle = "MediaTransCoder - Video";
            presetBox.ParentView = this;
            PreFillForm();
        }

        public void Refresh() {
            if (presetBox.UsePreset == true) {
                formatInput.IsEnabled = false;
                videoBox.IsEnabled = false;
                audioBox.IsEnabled = false;
                Preset? preset = presetBox.GetPreset();
                if(preset != null) {
                    if(preset.Options.Format != null) {
                        this.Format = preset.Options.Format.Value;
                    }
                    if(preset.Options.Video != null) {
                        videoBox.Video = preset.Options.Video;
                    }
                    if (preset.Options.Audio != null) {
                        audioBox.Audio = preset.Options.Audio;
                    }
                }
            } else {
                formatInput.IsEnabled = true;
                videoBox.IsEnabled = true;
                audioBox.IsEnabled = true;
            }
        }

        private void PreFillForm() {
            if(presetBox.UsePreset == true) {
                formatInput.IsEnabled = false;
                videoBox.IsEnabled = false;
                audioBox.IsEnabled = false;
            } else {
                formatInput.IsEnabled = true;
                videoBox.IsEnabled = true;
                audioBox.IsEnabled = true;
            }
            Format = ContainerFormat.matroska;
            List<ContainerFormat> formats = EnumHelper.GetVideoFormats();
            foreach(ContainerFormat format in formats) {
                formatInput.Items.Add(EnumHelper.GetName(format));
            }
            if (formatInput.Items.Contains(EnumHelper.GetName(format))){
                formatInput.SelectedIndex = formatInput.Items.IndexOf(EnumHelper.GetName(format));
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            window.SetMenuView();
        }

        private void formatInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<ContainerFormat> formats = EnumHelper.GetVideoFormats();
            foreach (ContainerFormat format in formats) {
                if(EnumHelper.GetName(format) == formatInput.SelectedItem.ToString()) {
                    this.Format = format;
                    break;
                }
            }
        }

        private bool Validate() {
            if (!inputBox.IsValid) {
                context.Display?.Send("Nieprawidłowe ustawienia plików źródłowych i docelowych!", MessageType.ERROR);
                return false;
            }
            if(videoBox.Video == null) {
                context.Display?.Send("Nieprawidłowe ustawienia wideo!", MessageType.ERROR);
                return false;
            }
            if (audioBox.Audio == null) {
                context.Display?.Send("Nieprawidłowe ustawienia audio!", MessageType.ERROR);
                return false;
            }
            return true;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e) {
            if(Validate()) {
                EndpointOptions args = new EndpointOptions();
                args.InputOption = inputBox.Input.Value;
                args.Input = inputBox.InputPath;
                args.Output = inputBox.OutputPath;
                args.OverrideExistingFiles = true;
                args.SkipExistingFiles = false;
                args.AudioOnly = false;
                args.AllowDirectoryCreation = true;
                args.Acceleration = context.DefaultAcceleration;
                args.Format = format;
                args.Video = videoBox.Video;
                args.Audio = audioBox.Audio;
                args.Image = null;
                context.Backend?.ConvertVideo(args);
            }
        }
    }
}
