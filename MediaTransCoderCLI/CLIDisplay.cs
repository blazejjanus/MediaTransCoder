using MediaTransCoder.Backend;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MediaTransCoder.CLI {
    delegate string? ConsoleReader();

    internal class CLIDisplay : IDisplay {
        private static CLIDisplay? instance;
        public ProgressBar? Progress { get; set; }
        private bool WasTimeout { get; set; }

        private CLIDisplay() {
            WasTimeout = false;
        }

        public static CLIDisplay GetInstance() {
            if (instance == null) {
                instance = new CLIDisplay();
            }
            return instance;
        }

        public void Send(string message, MessageType type = MessageType.INFO) {
            SetColor(type);
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void UpdateProgress(double progress) {
            Progress?.Update(progress, true);
        }

        public string Read(string message, string defaultValue = "") {
            Console.WriteLine(message);
            string? result = null;
            var timeout = GetTimer();
            while (true) {
                timeout.Start();
                if(Console.KeyAvailable) {
                    result = Console.ReadLine();
                }
                if(result != null) {
                    result = result.Trim();
                    timeout.Stop();
                    return result;
                }
                if(WasTimeout) {
                    Send("No user interaction fro 10s, default value will be returned.", MessageType.WARNING);
                    timeout.Stop();
                    WasTimeout = false;
                    return defaultValue;
                }
            }
        }

        public bool GetBool(string message) {
            var result = string.Empty;
            while (true) {
                result = Read(message, "n");
                if(result == "y" || result == "t") {
                    return true;
                }
                if (result == "n") {
                    return false;
                }
                Console.WriteLine("Invalid value provided!");
            }
        }

        private void TimerCallback(object source, ElapsedEventArgs e) {
            WasTimeout = true;
        }

        private Timer GetTimer() {
            var result = new Timer();
            result.Elapsed += new ElapsedEventHandler(TimerCallback);
            result.Interval = 10000;
            result.Enabled = true;
            return result;
        }

        private void SetColor(MessageType type) {
            switch (type) {
                case MessageType.WARNING:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case MessageType.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.SUCCESS:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case MessageType.DEBUG:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
            }
        }
    }
}
