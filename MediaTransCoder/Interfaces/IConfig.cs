namespace MediaTransCoder.Backend {
    public interface IConfig {
        /// <summary>
        /// Set config instance
        /// </summary>
        /// <param name="config"></param>
        public static void SetConfig(IConfig config) { }
        /// <summary>
        /// Get config instance
        /// </summary>
        /// <returns>Instance of class implementing IConfig for currently used GUI type</returns>
        public static IConfig GetConfig() { return null; }

        public LoggingLevel Logging { get; set; }
    }
}
