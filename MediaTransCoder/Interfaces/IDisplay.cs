namespace MediaTransCoder.Backend {
    public interface IDisplay {
        /// <summary>
        /// Read string value from user
        /// </summary>
        /// <param name="message">Message displayed to user</param>
        /// <param name="defaultValue">Default expected value (optional)</param>
        /// <returns>User string input</returns>
        public string Read(string message, string defaultValue = "");
        /// <summary>
        /// Read bool value from user
        /// </summary>
        /// <param name="message">Message displayed to user</param>
        /// <returns>User bool input</returns>
        public bool GetBool(string message);
        /// <summary>
        /// Dspaly message to user
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type">Type of message</param>
        public void Send(string message, MessageType type = MessageType.INFO);
        /// <summary>
        /// Update conversion progress in UI
        /// </summary>
        /// <param name="progress">Progress in %</param>
        public void UpdateProgress(double progress);
        /// <summary>
        /// Display conversion result
        /// </summary>
        /// <param name="results">conversion result</param>
        public void ShowResults(Measurer results);
    }
}
