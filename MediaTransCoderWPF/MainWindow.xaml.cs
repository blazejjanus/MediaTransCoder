using MediaTransCoder.WPF.Views;
using System.Windows;

namespace MediaTransCoder.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly MenuView menuView;
        private readonly AudioView audioView;
        private readonly VideoView videoView;
        private readonly ImageView imageView;
        private readonly SettingsView settingsView;
        public MainWindow() {
            InitializeComponent();
            audioView = new AudioView(this);
            videoView = new VideoView(this);
            imageView = new ImageView(this);
            settingsView = new SettingsView(this);
            menuView = new MenuView(this);
            SetMenuView();
        }

        public void SetMenuView() {
            content.Content = menuView;
            Title = menuView.WindowTitle;
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
