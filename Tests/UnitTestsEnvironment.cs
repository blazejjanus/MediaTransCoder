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
        }

        public static UnitTestsEnvironment Get() {
            return instance ?? (instance = new UnitTestsEnvironment());
        }
    }
}
