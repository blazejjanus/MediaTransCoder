using MediaTransCoder.Backend;

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
            }
        }

        public void UpdateProgress(double progress) {
            Progress?.Update(progress, true);
        }
    }
}
