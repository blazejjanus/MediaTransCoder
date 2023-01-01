using MediaTransCoder.Backend;
using System.Diagnostics;

namespace MediaTransCoder.Tests {
    internal class MockDisplay : IDisplay {
        private static MockDisplay? instance;
        private MockDisplay() { }

        public static MockDisplay GetInstance() {
            if (instance == null) {
                instance = new MockDisplay();
            }
            return instance;
        }

        public bool GetBool(string message) {
            Debug.WriteLine(message);
            return true;
        }

        public string Read(string message) {
            Debug.WriteLine(message);
            return "";
        }

        public void Send(string message, MessageType type = MessageType.INFO) {
            Debug.WriteLine(type.ToString() + ": " + message);
        }

        public void UpdateProgress(double progress) {
            Debug.WriteLine(progress);
        }
    }
}
