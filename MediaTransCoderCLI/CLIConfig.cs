using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    public class CLIConfig : IConfig {
        private CLIConfig() {
            Logging = LoggingLevel.INFO;
        }
        public LoggingLevel Logging { get; set; }

        public static IConfig GetConfig() {
            if (instance == null) {
                instance = new CLIConfig();
            }
            return instance;
        }

        public static void SetConfig(IConfig config) {
            throw new NotImplementedException();
        }

        private static IConfig instance;
    }
}
