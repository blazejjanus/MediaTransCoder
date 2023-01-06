using System.Windows;

namespace MediaTransCoder.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        internal WPFConfig? Config { get; set; }
        internal WPFContext? Context { get; set; }

        protected override void OnStartup(StartupEventArgs e) {
            Config = WPFConfig.TryRead();
            Context = WPFContext.Get();
            base.OnStartup(e);
        }
    }
}
