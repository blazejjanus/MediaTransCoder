using System.Text.Json.Serialization;

namespace MediaTransCoder.Backend {
    public class LoggingConfig {
        [JsonPropertyName("LoggingLevel")]
        public LoggingLevel LoggingLevel { get; set; }
        [JsonPropertyName("LogFilePath")]
        public string? LogFilePath { get; set; }
    }
}
