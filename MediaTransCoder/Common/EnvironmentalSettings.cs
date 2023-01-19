namespace MediaTransCoder.Backend {
    public class EnvironmentalSettings {
        private static EnvironmentalSettings? instance;
        private EnvironmentalSettings() {
            string dataDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            RootPath = dataDirectoryPath;
            if (dataDirectoryPath.Contains(".build")) {
                RootPath = dataDirectoryPath.Split(".build").First();
            } else {
                if (dataDirectoryPath.Contains(".publish")) {
                    RootPath = dataDirectoryPath.Split(".publish").First();
                } else {
                    RootPath = dataDirectoryPath;
                }
            }
            LogPath = Path.Combine(RootPath, ".logs");
            if(!File.Exists(Path.Combine(RootPath, ".config.josn"))){
                ConfigPath = RootPath;
            } else {
                if (!File.Exists(Path.Combine(RootPath, ".config.josn"))) {
                    ConfigPath = RootPath;
                } else {
                    ConfigPath = Path.Combine(RootPath, ".config");
                }
            }
        }
        public string RootPath { get; set; }
        public string LogPath { get; set; }
        public string ConfigPath { get; set; }
        public static EnvironmentalSettings Get() {
            if (instance == null) {
                instance = new EnvironmentalSettings();
            }
            return instance;
        }
    }
}
