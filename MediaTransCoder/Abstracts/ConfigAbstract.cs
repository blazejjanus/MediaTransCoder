using System.Text.Json.Serialization;

namespace MediaTransCoder.Backend {
    public abstract class ConfigAbstract {
        [JsonIgnore]
        public static string version = "0.1";
        [JsonPropertyName("EnvironmentType")]
        public EnvironmentType Environment { get; set; }
        [JsonPropertyName("Logging")]
        public LoggingConfig Logging { get; set; }
        public string BackendVersion { get; set; }
        public InterfaceConfig Interface { get; set; }

        public ConfigAbstract() {
            Logging = new LoggingConfig();
            BackendVersion = version;
            Interface = new InterfaceConfig();
        }
    }
}
