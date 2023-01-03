namespace MediaTransCoder.Backend {
    public interface IDisplay {
        //TODO: Add comments
        public string Read(string message, string defaultValue = "");
        public bool GetBool(string message);
        public void Send(string message, MessageType type = MessageType.INFO);
        public void UpdateProgress(double progress);
    }
}
