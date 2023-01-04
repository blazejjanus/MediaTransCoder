using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    internal class TryFfmpeg {
        private Endpoint Backend;
        private static CLIDisplay GUI = CLIDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(GUI);

        public TryFfmpeg(Endpoint backend) {
            Backend = backend;
        }

        public void Audio(EndpointOptions options, string? name = null) {
            if (name == null) {
                name = options.Format + " " + options.Audio?.Codec;
            }
            try {
                Backend?.ConvertAudio(options);
                foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                    Validator.Validate(file.Output);
                    RenameResultingFile(file, name);
                }
                GUI.Log("OK\n", MessageType.SUCCESS);
                if(File.Exists(name)) { }
            } catch (Exception exc) {
                string errorMessage = "Error while processing: " + name;
                errorMessage += "\nException:\n" + exc.Message;
                if (exc.InnerException != null) {
                    errorMessage += "\nInner:\n" + exc.InnerException.Message;
                }
                errorMessage += "\n\n";
                GUI.Log(errorMessage, MessageType.ERROR);
            }
        }

        public void Video(EndpointOptions options, string? name = null) {
            if(name == null) {
                name = options.Format + " " + options.Video?.Codec + " " + options.Audio?.Codec;
            }
            try {
                Backend?.ConvertVideo(options);
                foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                    Validator.Validate(file.Output);
                    RenameResultingFile(file, name);
                }
                GUI.Log("OK\n", MessageType.SUCCESS);
            } catch (Exception exc) {
                string errorMessage = "Error while processing: " + name;
                errorMessage += "\nException:\n" + exc.Message;
                if (exc.InnerException != null) {
                    errorMessage += "\nInner:\n" + exc.InnerException.Message;
                }
                errorMessage += "\n\n";
                GUI.Log(errorMessage, MessageType.ERROR);
            }
        }

        private void RenameResultingFile(FileOption file, string name) {
            string? path = Path.GetDirectoryName(file.Output);
            if(path != null) {
                name += Path.GetExtension(file.Output);
                path = Path.Combine(path, name);
                File.Move(file.Output, path);
            }
        }
    }
}
