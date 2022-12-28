using System.Text.Json.Serialization;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Configuration connected to Logging
    /// </summary>
    public class LoggingConfig {
        /// <summary>
        /// Determines logging level
        /// </summary>
        [JsonPropertyName("LoggingLevel")]
        public LoggingLevel LoggingLevel { get; set; }
        /// <summary>
        /// Determines if events should be logged to file. Provide fiel path or null to disable logging.
        /// </summary>
        [JsonPropertyName("LogFilePath")]
        public string? LogFilePath { get; set; }

        public LoggingConfig() {
            LogFilePath = EnvironmentalSettings.Get().LogPath;
            LoggingLevel = LoggingLevel.WARNING;
        }
    }
}
