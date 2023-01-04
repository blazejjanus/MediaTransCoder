namespace MediaTransCoder.Shared {
    public class TestingEnvironment {
        private static TestingEnvironment? instance;
        public static string? RootPath { get; set; }
        public string CurrentRootPath {
            get {
                return RootPath ?? Path.GetDirectoryName(Video.Input) ?? string.Empty;
            }
        }
        public PathInfo Video { get; private set; }
        public PathInfo Audio { get; private set; }
        public PathInfo Image { get; private set; }

        private TestingEnvironment() {
            if (RootPath == null) {
                throw new Exception("Define static RootPath before using TestingEnvironment");
            }
            Video = new PathInfo(RootPath + @"\input\video\sample.mp4", RootPath + @"\output\video\");
            Audio = new PathInfo(RootPath + @"\input\audio\sample.mp3", RootPath + @"\output\audio\");
            Image = new PathInfo(RootPath + @"\input\image\sample.jpg", RootPath + @"\output\image\");
        }

        public static TestingEnvironment Get() {
            return instance ?? (instance = new TestingEnvironment());
        }
    }
}
