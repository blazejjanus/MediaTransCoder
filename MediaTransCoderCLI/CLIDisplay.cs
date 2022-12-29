﻿using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    internal class CLIDisplay : IDisplay {
        private static CLIDisplay? instance;
        public ProgressBar? Progress { get; set; }
        private CLIDisplay() {}

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

        public string Read(string message) {
            Console.WriteLine(message);
            string? result = null;
            while (true) {
                result = Console.ReadLine();
                if(result != null) {
                    result = result.Trim();
                    return result;
                }
            }
        }

        public bool GetBool(string message) {
            var result = string.Empty;
            while (true) {
                result = Read(message);
                if(result == "y" || result == "t") {
                    return true;
                }
                if (result == "n") {
                    return false;
                }
                Console.WriteLine("Invalid value provided!");
            }
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
