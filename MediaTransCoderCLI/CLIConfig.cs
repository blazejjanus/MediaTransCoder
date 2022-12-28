using MediaTransCoder.Backend;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediaTransCoder.CLI
{
    public class CLIConfig {
        [JsonPropertyName("Backend")]
        public BackendConfig Backend { get; set; }
        public CLIConfig() { 
            Backend = new BackendConfig();
        }
        public static CLIConfig GetConfig() {
            if (instance == null) {
                instance = new CLIConfig();
            }
            return instance;
        }
        private static void Set(CLIConfig config) {
            instance = config;
        }
        public static CLIConfig ReadConfig() {
            var env = EnvironmentalSettings.Get();
            if(!File.Exists(env.ConfigPath + "config.json")) {
                throw new FileNotFoundException(env.ConfigPath + "config.json");
            }
            var config = JsonSerializer.Deserialize<CLIConfig>(File.ReadAllText(env.ConfigPath + "config.json"));
            if (config != null) {
                Set(config);
                return config;
            } else {
                throw new Exception("Cannot read config!");
            }
        }
        public static CLIConfig Defaults() {
            instance = new CLIConfig();
            return instance;
        }
        public void SaveConfig(string path) {
            if(instance == null) { throw new Exception("Config instance was null!"); }
            string json = JsonSerializer.Serialize<CLIConfig>(instance, 
                new JsonSerializerOptions() { WriteIndented= true });
            if (File.Exists(path)) {
                if(File.Exists(path + ".backup")) {
                    File.Delete(path + ".backup");
                }
                File.Move(path, path + ".backup");
                File.WriteAllText(path, json);
            }
        }
        [JsonIgnore]
        private static CLIConfig? instance;
    }
}
