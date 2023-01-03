namespace MediaTransCoder.Backend {
    public class ConversionValidator {
        private IDisplay? Display { get; set; }

        public ConversionValidator(IDisplay? display = null) {
            Display = display;
        }

        public bool IsValid(string path, bool deleteInvalid = true) {
            try {
                Validate(path, deleteInvalid);
                return true;
            }catch(Exception exc) {
                Display?.Send(exc.Message, MessageType.ERROR);
                return false;
            }
        }

        public void Validate(string path, bool deleteInvalid = true) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException(path);
            }
            var ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext)) {
                if(deleteInvalid) {
                    File.Delete(path);
                }
                throw new Exception("File doesn't have a valid extension!", new Exception(ext + " is not a valid extension."));
            }
            var info = new FileInfo(path);
            if(info.Length < 512) {
                if (deleteInvalid) {
                    File.Delete(path);
                }
                throw new Exception("File length is too short!");
            }
        }

        public void RemoveEmptyDirs(string dirPath) {
            foreach (string directory in Directory.GetDirectories(dirPath)) {
                RemoveEmptyDirs(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0) {
                    Directory.Delete(directory, false);
                    Display?.Send("Removed directory " + directory, MessageType.WARNING);
                }
            }
        }
    }
}
