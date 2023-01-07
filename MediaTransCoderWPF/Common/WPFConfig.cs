using MediaTransCoder.Backend;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediaTransCoder.WPF {
    internal class WPFConfig {
        [JsonPropertyName("Backend")]
        public BackendConfig Backend { get; set; }
        [JsonIgnore]
        private static WPFConfig? instance;

        public WPFConfig() {
            Backend = new BackendConfig();
        }

        public static WPFConfig GetConfig() {
            if (instance == null) {
                instance = new WPFConfig();
            }
            return instance;
        }

        private static void Set(WPFConfig config) {
            instance = config;
        }

        public static WPFConfig ReadConfig() {
            var env = EnvironmentalSettings.Get();
            if (!File.Exists(env.ConfigPath + "config.json")) {
                throw new FileNotFoundException(env.ConfigPath + "config.json");
            }
            var config = JsonSerializer.Deserialize<WPFConfig>(File.ReadAllText(env.ConfigPath + "config.json"));
            if (config != null) {
                Set(config);
                return config;
            } else {
                throw new Exception("Cannot read config!");
            }
        }

        public static WPFConfig TryRead() {
            try
            {
                instance = ReadConfig();
            } catch (Exception exc) {
                Debug.WriteLine(exc.Message);
                instance = Defaults();
                instance.SaveConfig();
            }
            return instance;
        }

        public static WPFConfig Defaults() {
            instance = new WPFConfig();
            return instance;
        }

        public void SaveConfig(string? path = null) {
            var env = EnvironmentalSettings.Get();
            if (instance == null) { throw new Exception("Config instance was null!"); }
            string json = JsonSerializer.Serialize(instance,
                new JsonSerializerOptions() { WriteIndented = true });
            if (path == null) {
                path = env.ConfigPath + "config.json";
            }
            if (File.Exists(path)) {
                if (File.Exists(path + ".backup")) {
                    File.Delete(path + ".backup");
                }
                File.Move(path, path + ".backup");
                File.WriteAllText(path, json);
            }
        }
    }
}
