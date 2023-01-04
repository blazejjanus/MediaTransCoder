namespace MediaTransCoder.Shared {
    public class PathInfo {
        public string Input { get; set; }
        public string Output { get; set; }
        public string? InputDirectory {
            get {
                return Path.GetDirectoryName(Input);
            }
        }
        public string InputFile {
            get {
                return Path.GetFileName(Input);
            }
        }
        public string InputDirectoryOrDefault {
            get {
                return Path.GetDirectoryName(Input) ?? Input;
            }
        }

        public PathInfo(string inputPath, string outputPath) {
            Input = inputPath;
            Output = outputPath;
        }
    }
}
