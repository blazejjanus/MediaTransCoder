using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    public class CLIConfig: ConfigAbstract {
        private CLIConfig() {
            
        }

        public static ConfigAbstract GetConfig() {
            if (instance == null) {
                instance = new CLIConfig();
            }
            return instance;
        }

        public static void SetConfig(ConfigAbstract config) {
            throw new NotImplementedException();
        }

        private static ConfigAbstract instance;
    }
}
