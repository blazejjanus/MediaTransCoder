using MediaTransCoder.WPF.Views;
using System.Windows;
using System.Windows.Controls;

namespace MediaTransCoder.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        internal WPFContext Context { get; private set; }
        private readonly MenuView menuView;
        private AudioView audioView;
        private VideoView videoView;
        private ImageView imageView;
        private readonly SettingsView settingsView;

        public MainWindow() {
            InitializeComponent();
            Context = WPFContext.Get();
            audioView = new AudioView(this);
            videoView = new VideoView(this);
            imageView = new ImageView(this);
            settingsView = new SettingsView(this);
            menuView = new MenuView(this);
            SetMenuView();
        }

        public void SetMenuView(UserControl? sender = null) {
            content.Content = menuView;
            Title = menuView.WindowTitle;
            ClearSender(sender);
        }

        private void ClearSender(UserControl? sender) {
            if(sender != null) {
                if(sender.GetType() == typeof(AudioView)) {
                    audioView = new AudioView(this);
                }
                if (sender.GetType() == typeof(VideoView)) {
                    videoView = new VideoView(this);
                }
                if (sender.GetType() == typeof(ImageView)) {
                    imageView = new ImageView(this);
                }
            }
        }

        public void SetSettingsView() {
            content.Content = settingsView;
            Title = settingsView.WindowTitle;
        }

        public void SetAudioView() {
            content.Content = audioView;
            Title = audioView.WindowTitle;
        }
        public void SetVideoView() {
            content.Content = videoView;
            Title = videoView.WindowTitle;
        }

        public void SetImageView() {
            content.Content = imageView;
            Title = imageView.WindowTitle;
        }
    }
}
