namespace MediaTransCoder.Backend {
    public class FileOption {
        public string Input { get; set; }
        public string Output { get; set; }

        public FileOption() {
            Input = string.Empty;
            Output = string.Empty;
        }

        public FileOption(string input, string output) {
            Input = input;
            Output = output;
        }

        public FileOption(EndpointOptions options) {
            Input = options.Input;
            Output = options.Output;
        }

        /// <summary>
        /// Genrate list of FileOption for multiple input files with single directory output
        /// </summary>
        /// <param name="inputFiles">List of input files.</param>
        /// <param name="outputDirectory">Common output directory for all source files.</param>
        /// <returns>List of prepared FileOption entries</returns>
        public static List<FileOption> GetFileOptionsFromList(List<string> inputFiles, string outputDirectory) {
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
        /// <param name="searchCriteria">VideoExtension to search</param>
        /// <returns>List of prepared FileOption entries</returns>
        public static List<FileOption> GetFileOptionsFromDirectory(string inputDirectory, string outputDirectory, string searchCriteria, bool recursive = false) {
            var result = new List<FileOption>();
            var inputRoot = new DirectoryInfo(inputDirectory);
            List<FileInfo> inputFiles = new List<FileInfo>();
            if (recursive) {
                inputFiles = inputRoot.GetFiles(searchCriteria, SearchOption.AllDirectories).ToList();
            } else {
                inputFiles = inputRoot.GetFiles(searchCriteria, SearchOption.TopDirectoryOnly).ToList();
            }
            foreach (var inputFile in inputFiles) {
                result.Add(new FileOption() {
                    Input = inputFile.FullName,
                    Output = Path.Combine(outputDirectory, Path.GetRelativePath(inputFile.FullName, inputRoot.FullName))
                });
            }
            return result;
        }

        public static List<FileOption> GetFileOptionsFromDirectory(string inputDirectory, string outputDirectory, List<string> searchCriteria, bool recursive = false) {
            var result = new List<FileOption>();
            var inputRoot = new DirectoryInfo(inputDirectory);
            List<FileInfo> inputFiles = new List<FileInfo>();
            foreach (var criteria in searchCriteria) {
                if (recursive) {
                    inputFiles.AddRange(inputRoot.GetFiles(criteria, SearchOption.AllDirectories).ToList());
                } else {
                    inputFiles.AddRange(inputRoot.GetFiles(criteria, SearchOption.TopDirectoryOnly).ToList());
                }
            }
            foreach (var inputFile in inputFiles) {
                string relative = Path.GetRelativePath(inputRoot.FullName, inputFile.FullName);
                result.Add(new FileOption() {
                    Input = inputFile.FullName,
                    Output = Path.Combine(outputDirectory, Path.GetDirectoryName(relative) ?? "")
                });
            }
            return result;
        }

        public override string ToString() {
            return Input + " -> " + Output;
        }
    }
}
