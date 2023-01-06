using MediaTransCoder.Backend;
using System.Diagnostics;

namespace MediaTransCoder.Tests {
    public class TestDisplay : IDisplay {
        private static TestDisplay? instance;
        private static IDisplay? GUI = null;
        private string? logFile = null;
        public string? LogFile {
            get {
                return logFile;
            }
            set {
                logFile = value;
                if (File.Exists(logFile)) {
                    if (File.Exists(logFile + ".old")) {
                        File.Delete(logFile + ".old");
                    }
                    File.Move(logFile, logFile + ".old");
                }
            }
        }
        public bool ShowProgress { get; set; } = true;

        private TestDisplay() { }

        public static TestDisplay GetInstance() {
            return instance ?? (instance = new TestDisplay());
        }

        public void Init(IDisplay gui) {
            GUI = gui;
        }

        public void Init(IDisplay gui, string logFile) {
            GUI = gui;
            LogFile = logFile;
        }

        public bool GetBool(string message) {
            Display(message);
            if(GUI != null) {
                return GUI.GetBool(message);
            }
            return true;
        }

        public string Read(string message, string defaultValue = "") {
            Display(message);
            if (GUI != null) {
                return GUI.Read(message);
            }
            return defaultValue;
        }

        public void Send(string message, MessageType type = MessageType.INFO) {
            Display(message);
        }

        public void UpdateProgress(double progress) {
            if(ShowProgress) {
                Display("Progress: " + progress);
            }
        }

        public void ShowResults(Measurer results) {
            Log(results.GetStats());
        }

        public void Log(string message, MessageType type = MessageType.INFO) {
            Display(message, type);
            if (LogFile != null) {
                File.AppendAllText(LogFile, message + "\n");
            }
        }

        private void Display(string message, MessageType type = MessageType.INFO) {
            Debug.WriteLine(message);
            if(GUI != null) {
                GUI.Send(message, type);
            }
        }
    }
}
