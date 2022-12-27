namespace MediaTransCoder.Backend {
    public class FileOption {
        string Input { get; set; }
        string Output { get; set; }

        public FileOption() {
            Input = string.Empty;
            Output = string.Empty;
        }

        /// <summary>
        /// Genrate list of FileOption for multiple input files with single directory output
        /// </summary>
        /// <param name="inputFiles">List of input files.</param>
        /// <param name="outputDirectory">Common output directory for all source files.</param>
        /// <returns>List of prepared FileOption entries</returns>
        public List<FileOption> GetFileOptions(List<string> inputFiles, string outputDirectory) {
            var result = new List<FileOption>();
            foreach (var inputFile in inputFiles) {
                if (!File.Exists(inputFile)) {
                    throw new FileNotFoundException(inputFile);
                }
                result.Add(new FileOption() {
                    Input = inputFile,
                    Output = Path.Combine(outputDirectory, Path.GetFileName(inputFile))
                });
            }
            return result;
        }

        /// <summary>
        /// Genrate list of FileOption for all files matching specified criteria in input directory and all subdirectories
        /// </summary>
        /// <param name="inputDirectory">Input directory</param>
        /// <param name="outputDirectory">Output directory</param>
        /// <param name="searchCriteria">FileExtension to search</param>
        /// <returns>List of prepared FileOption entries</returns>
        public List<FileOption> GetFileOptionsRecursive(string inputDirectory, string outputDirectory, string searchCriteria = "*.*") {
            var result = new List<FileOption>();
            var inputRoot = new DirectoryInfo(inputDirectory);
            //TODO: Get Extensions of supported formats
            var inputFiles = inputRoot.GetFiles(searchCriteria, SearchOption.AllDirectories).ToList();
            foreach (var inputFile in inputFiles) {
                result.Add(new FileOption() {
                    Input = inputFile.FullName,
                    Output = Path.Combine(outputDirectory, Path.GetRelativePath(inputFile.FullName, inputRoot.FullName))
                });
            }
            return result;
        }
    }
}
