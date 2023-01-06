using MediaTransCoder.Backend;

namespace MediaTransCoder.Tests {
    internal class TryFfmpeg {
        public bool Verbose { get; set; } = false;
        private Endpoint Backend;
        private static TestDisplay Display = TestDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(Display);

        public TryFfmpeg(Endpoint backend) {
            Backend = backend;
        }

        public bool Audio(EndpointOptions options) {
            if(Verbose) {
                Display.Log(options.ToString(), MessageType.WARNING);
            }
            try {
                Backend?.ConvertAudio(options);
                foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                    Validator.Validate(file.Output);
                }
                Display.Log("OK\n", MessageType.SUCCESS);
                return true;
            } catch (Exception exc) {
                string errorMessage = "Error while processing: " + options.Format + " " + options.Audio?.Codec;
                errorMessage += "\nException:\n" + exc.Message;
                if (exc.InnerException != null) {
                    errorMessage += "\nInner:\n" + exc.InnerException.Message;
                }
                errorMessage += "\n\n";
                Display.Log(errorMessage, MessageType.ERROR);
                return false;
            }
        }

        public bool Video(EndpointOptions options) {
            if (Verbose) {
                Display.Log(options.ToString(), MessageType.WARNING);
            }
            try {
                Backend?.ConvertVideo(options);
                foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                    Validator.Validate(file.Output);
                }
                Display.Log("OK\n", MessageType.SUCCESS);
                return true;
            } catch (Exception exc) {
                string errorMessage = "Error while processing: " + options.Format + " " + options.Video?.Codec + " " + options.Audio?.Codec;
                errorMessage += "\nException:\n" + exc.Message;
                if (exc.InnerException != null) {
                    errorMessage += "\nInner:\n" + exc.InnerException.Message;
                }
                errorMessage += "\n\n";
                Display.Log(errorMessage, MessageType.ERROR);
                return false;
            }
        }

        public bool Image(EndpointOptions options) {
            if (Verbose) {
                Display.Log(options.ToString(), MessageType.WARNING);
            }
            try {
                Backend?.ConvertImage(options);
                foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                    Validator.Validate(file.Output);
                }
                Display.Log("OK\n", MessageType.SUCCESS);
                return true;
            } catch (Exception exc) {
                string errorMessage = "Error while processing: " + options.Format + " " + options.Video?.Codec + " " + options.Audio?.Codec;
                errorMessage += "\nException:\n" + exc.Message;
                if (exc.InnerException != null) {
                    errorMessage += "\nInner:\n" + exc.InnerException.Message;
                }
                errorMessage += "\n\n";
                Display.Log(errorMessage, MessageType.ERROR);
                return false;
            }
        }
    }
}
