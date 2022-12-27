using System.Text.Json.Serialization;

namespace MediaTransCoder.Backend {
    public class BackendConfig {
        /// <summary>
        /// Configuration connected to system environemnt
        /// </summary>
        [JsonPropertyName("EnvironmentType")]
        public EnvironmentType Environment { get; set; }

        /// <summary>
        /// Configuration connected to installed hardware (CPU and GPU) used in hardware acceleration
        /// </summary>
        [JsonPropertyName("Hardware")]
        public HardwareConfig Hardware { get; set; }

        /// <summary>
        /// Configuration connected to Logging
        /// </summary>
        [JsonPropertyName("Logging")]
        public LoggingConfig Logging { get; set; }

        /// <summary>
        /// Configuration connected to interface calling backend
        /// </summary>
        [JsonPropertyName("Interface")]
        public InterfaceConfig Interface { get; set; }

        /// <summary>
        /// Path or command to call ffmpeg
        /// </summary>
        [JsonPropertyName("FfmpegPath")]
        public string? FfmpegPath { get; set; }

        /// <summary>
        /// Temp directory path, if none specified system temp will be used
        /// </summary>
        [JsonPropertyName("TempDirPath")]
        public string? TempDirPath { get; set; }

        public BackendConfig() {
            Logging = new LoggingConfig();
            Interface = new InterfaceConfig();
            Hardware = new HardwareConfig();
            Environment = EnvironmentType.Production;
            FfmpegPath = "ffmpeg";
            if(TempDirPath == null)
                TempDirPath = Path.GetTempPath();
        }
    }
}
