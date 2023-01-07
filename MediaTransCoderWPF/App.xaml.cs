using MediaTransCoder.Backend;
using System.IO;
using System.Windows;

namespace MediaTransCoder.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        internal WPFContext? Context { get; set; }

        protected override void OnStartup(StartupEventArgs e) {
            Setup();
            Context = WPFContext.Get();
            base.OnStartup(e);
        }

        private void Setup() {
            var env = EnvironmentalSettings.Get();
            Context = WPFContext.Get();
            Context.Display = WPFDisplay.GetInstance();
            WPFConfig config = new WPFConfig();
            if (File.Exists(env.ConfigPath + "config.json")) {
                config = WPFConfig.ReadConfig();
            } else {
                config.Backend = Endpoint.FirstTimeSetup();
                if(config.Backend.FfmpegPath == null) {
                    Context.Display.Send("Nie znaleziono Ffmpeg! Upewnij się, ze został on zainstalowany, oraz że jest dodany do zmiennych środowiskowych, lub jego ścieżka znajduje się w pliku config.json");
                }
                config.SaveConfig();
            }
            Context.Config = config.Backend;
            Context.Backend = new Endpoint(Context.Config, Context.Display);
        }
    }
}
