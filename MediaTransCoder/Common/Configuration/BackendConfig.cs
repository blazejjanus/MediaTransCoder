using System.Text.Json;
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
        /// Configuration connected to interface calling backend
        /// </summary>
        [JsonPropertyName("Interface")]
        public InterfaceConfig Interface { get; set; }

        /// <summary>
        /// Path or command to call ffmpeg
        /// </summary>
        [JsonPropertyName("FfmpegPath")]
        public string? FfmpegPath { get; set; }

        public BackendConfig() {
            Interface = new InterfaceConfig();
            Hardware = new HardwareConfig();
            Environment = EnvironmentType.Production;
            FfmpegPath = "ffmpeg";
        }
    }
}
