using MediaTransCoder.Backend;
using System.Windows;

namespace MediaTransCoder.WPF {
    internal class WPFDisplay : IDisplay {
        public RedirectMessage? Target { get; set; }
        public bool RedirectOutput { get; set; } = false;
        private static WPFDisplay? instance;

        private WPFDisplay() { }

        public static WPFDisplay GetInstance() {
            return instance ?? (instance = new WPFDisplay());
        }

        public bool GetBool(string message) {
            string caption = "MediaTransCoder";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
            switch (result) {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }
        }

        public string Read(string message, string defaultValue = "") {
            return defaultValue;
        }

        public void Send(string message, MessageType type = MessageType.INFO) {
            if (Target == null || RedirectOutput == false) {
                string caption = "MediaTransCoder";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(message, caption, button, icon, MessageBoxResult.OK);
            } else {
                if(type == MessageType.WARNING || type == MessageType.ERROR) {
                    Target(type.ToString() + ": " + message);
                } else {
                    Target(message);
                }
            }
        }

        public void ShowResults(Measurer results) {
            if (Target == null || RedirectOutput == false) {
                string caption = "Conversion result";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(results.GetStats(), caption, button, icon, MessageBoxResult.OK);
            } else {
                Target(results.GetStats());
            }
        }

        public void UpdateProgress(double progress) {
            
        }
    }
}
