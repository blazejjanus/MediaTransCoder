namespace MediaTransCoder.Backend {
    public interface IDisplay {
        /// <summary>
        /// Returns GUI instance
        /// </summary>
        /// <returns>GUI instance</returns>
        public IDisplay GetInstance();
        //TODO Rethink this method
        //COMMENT
        public string Read(object resource);
        //COMMENT
        public void Send(string message, MessageType type=MessageType.INFO);
    }
}
