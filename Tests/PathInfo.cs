namespace MediaTransCoder.Tests {
    internal class PathInfo {
        internal string Input { get; set; }
        internal string Output { get; set; }
        internal string? InputDirectory {
            get {
                return Path.GetDirectoryName(Input);
            }
        }
        internal string? InputFile {
            get {
                return Path.GetFileName(Input);
            }
        }

        internal PathInfo(string inputPath, string outputPath) {
            Input = inputPath;
            Output = outputPath;
        }
    }
}
