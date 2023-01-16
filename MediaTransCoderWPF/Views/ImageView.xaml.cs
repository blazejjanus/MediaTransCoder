using MediaTransCoder.Backend;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MediaTransCoder.WPF.Views {
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl, IRefreshableControl {
        public string WindowTitle { get; private set; }
        private readonly MainWindow window;

        public ImageView(MainWindow window) {
            InitializeComponent();
            this.window = window;
            WindowTitle = "MediaTransCoder - Image";
            presetBox.ParentView = this;
            if (window.Context.Display == null) {
                throw new NullReferenceException();
            }
            window.Context.Display.Target = DisplayOutput;
            PreFillForm();
        }

        public void DisplayOutput(string message) {
            this.Dispatcher.Invoke(() => {
                resultText.Text += "\n" + message;
            });
        }

        public void PreFillForm() {
            resultsScroll.Visibility = Visibility.Hidden;
            resultText.Text = "";
            if (presetBox.UsePreset == true) {
                imageControl.IsEnabled = false;
            } else {
                imageControl.IsEnabled = true;
            }
        }

        public void Refresh() {
            if (presetBox.UsePreset == true) {
                imageControl.IsEnabled = false;
                Preset? preset = presetBox.GetPreset();
                if (preset != null) {
                    if (preset.Options.Image != null) {
                        imageControl.Image = preset.Options.Image;
                    }
                }
            } else {
                imageControl.IsEnabled = true;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            window.SetMenuView(this);
        }

        private bool Validate() {
            if (!inputBox.IsValid) {
                window.Context.Display?.Send("Nieprawidłowe ustawienia plików źródłowych i docelowych!", MessageType.ERROR);
                return false;
            }
            if (imageControl.Image == null) {
                window.Context.Display?.Send("Nieprawidłowe ustawienia obrazu!", MessageType.ERROR);
                return false;
            }
            return true;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e) {
            if (Validate()) {
                if (window.Context.Display == null) {
                    throw new NullReferenceException();
                }
                if (inputBox.Input == null) {
                    throw new NullReferenceException();
                }
                if (inputBox.InputPath == null) {
                    throw new NullReferenceException();
                }
                if (inputBox.OutputPath == null) {
                    throw new NullReferenceException();
                }
                if (window.Context.Backend == null) {
                    throw new Exception("Nie można wywołać backendu!");
                }
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
                args.Format = null;
                args.Audio = null;
                args.Video = null;
                args.Image = imageControl.Image;
                backButton.IsEnabled = false;
                resultsScroll.Visibility = Visibility.Visible;
                try {
                    Thread thread = new Thread(() => window.Context.Backend.ConvertImage(args));
                    thread.Start();
                } catch (Exception exc) {
                    window.Context.Display.RedirectOutput = false;
                    window.Context.Display.Send(exc.Message, MessageType.ERROR);
                    PreFillForm();
                }
                backButton.IsEnabled = true;
                window.Context.Display.RedirectOutput = false;
                PreFillForm();
            }
        }
    }
}
