using MediaTransCoder.Backend;
using MediaTransCoder.CLI;

namespace MediaTransCoder.Tests {
    internal class UnitTestsEnvironment {
        private static UnitTestsEnvironment? instance;
        public PathInfo Video { get; private set; }
        public PathInfo Audio { get; private set; }
        public PathInfo Image { get; private set; }

        private UnitTestsEnvironment() {
            Video = new PathInfo(@"E:\TEMP\mtc\input\video\sample.mp4", @"E:\TEMP\mtc\output\video\");
            Audio = new PathInfo(@"E:\TEMP\mtc\input\audio\sample.mp3", @"E:\TEMP\mtc\output\audio\");
            Image = new PathInfo(@"E:\TEMP\mtc\input\image\sample.jpg", @"E:\TEMP\mtc\output\image\");
            MockContext();
        }

        public static UnitTestsEnvironment Get() {
            return instance ?? (instance = new UnitTestsEnvironment());
        }

        internal void MockContext() {
            if (!Context.IsSet) {
                var config = CLIConfig.ReadConfig();
                if (config == null) {
                    throw new Exception("Obtained config was null!");
                }
                Context.Init(config.Backend, MockDisplay.GetInstance());
            }
        }
    }
}
