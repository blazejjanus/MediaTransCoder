using MediaTransCoder.Backend;
using MediaTransCoder.Shared;
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
            TestingEnvironment.RootPath = env.RootPath;
            Context = WPFContext.Get();
            Context.Display = WPFDisplay.GetInstance();
            WPFConfig config = WPFConfig.TryRead();
            if (!config.WasRead) {
                config.Backend = Endpoint.FirstTimeSetup();
                Context.Display.Send("Nie można odczytać ustawień, zostaną wprowadzone ustawienia domyślne.", MessageType.WARNING);
                if (config.Backend.FfmpegPath == null) {
                    Context.Display.Send("Nie znaleziono Ffmpeg! Upewnij się, ze został on zainstalowany, oraz że jest dodany do zmiennych środowiskowych, lub jego ścieżka znajduje się w pliku config.json");
                }
                config.SaveConfig();
            }
            /*
            if (File.Exists(env.ConfigPath + "config.json")) {
                config = WPFConfig.ReadConfig();
            } else {
                config.Backend = Endpoint.FirstTimeSetup();
                Context.Display.Send("Nie można odczytać ustawień, zostaną wprowadzone ustawienia domyślne.", MessageType.WARNING);
                if(config.Backend.FfmpegPath == null) {
                    Context.Display.Send("Nie znaleziono Ffmpeg! Upewnij się, ze został on zainstalowany, oraz że jest dodany do zmiennych środowiskowych, lub jego ścieżka znajduje się w pliku config.json");
                }
                config.SaveConfig();
            }
            */
            Context.Config = config;
            Context.Backend = new Endpoint(Context.Config.Backend, Context.Display);
            Context.Backend.IsDebug = false;
        }
    }
}
