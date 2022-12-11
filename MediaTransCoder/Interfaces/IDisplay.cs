namespace MediaTransCoder.Backend {
    public interface IDisplay {
        //COMMENT
        public string Read(object resource);
        //COMMENT
        public void Send(string message, MessageType type=MessageType.INFO);
    }
}
