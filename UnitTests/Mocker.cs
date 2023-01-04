using MediaTransCoder.CLI;

namespace MediaTransCoder.UnitTests {
    internal static class Mocker {
        internal static void MockContext() {
            if (!Context.IsSet) {
                var config = CLIConfig.ReadConfig();
                if (config == null) {
                    throw new Exception("Obtained config was null!");
                }
                Context.Init(config.Backend, TestDisplay.GetInstance());
            }
        }
    }
}
