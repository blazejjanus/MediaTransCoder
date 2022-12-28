namespace MediaTransCoder.Backend {
    public interface IDisplay {
        //TODO: Add comments
        public void Send(string message, MessageType type=MessageType.INFO);
        public void UpdateProgress(double progress);
    }
}
