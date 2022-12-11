using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    internal class CLIDisplay : IDisplay {
        private CLIDisplay() { }
        public static IDisplay GetInstance() {
            if (instance == null) {
                instance = new CLIDisplay();
            }
            return instance;
        }

        public string Read(object resource) {
            throw new NotImplementedException();
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
        private static IDisplay? instance;
    }
}
