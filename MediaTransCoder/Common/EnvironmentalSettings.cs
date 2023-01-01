namespace MediaTransCoder.Backend {
    public class EnvironmentalSettings {
        private static EnvironmentalSettings? instance;
        private EnvironmentalSettings() {
            string dataDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            RootPath = dataDirectoryPath;
            if (dataDirectoryPath.Contains(".build")) {
                RootPath = dataDirectoryPath.Split(".build").First();
            }
            if (dataDirectoryPath.Contains(".publish")) {
                RootPath = dataDirectoryPath.Split(".publish").First();
            }
            LogPath = RootPath + ".logs\\";
            ConfigPath = RootPath + ".config\\";
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
