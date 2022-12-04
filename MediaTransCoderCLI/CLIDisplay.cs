using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    internal class CLIDisplay : IDisplay {
        private CLIDisplay() { }
        public IDisplay GetInstance() {
            if (instance == null) {
                instance = new CLIDisplay();
            }
            return instance;
        }

        public string Read(object resource) {
            throw new NotImplementedException();
        }

        public void Send(string message, MessageType type=MessageType.INFO) {
            throw new NotImplementedException();
        }

        private IDisplay instance;
    }
}
