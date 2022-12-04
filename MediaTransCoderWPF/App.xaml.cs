using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace MediaTransCoder.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication {
        protected override Window CreateShell() {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            //Used to register used types
        }
    }
}
