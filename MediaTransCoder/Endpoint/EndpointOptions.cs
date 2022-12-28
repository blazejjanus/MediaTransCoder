namespace MediaTransCoder.Backend {
    public class EndpointOptions {
        public bool OverrideExistingFiles { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public InputOptions InputOption { get; set; }
        public HardwareAcceleration Acceleration { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? Audio { get; set; }
        public VideoOptions? Video { get; set; }

        public EndpointOptions() {
            OverrideExistingFiles = true;
            Acceleration = HardwareAcceleration.NONE;
            Input = string.Empty;
            Output = string.Empty;
            InputOption = InputOptions.FILE;
        }

        private void Validate() {
            switch (InputOption) {
                case InputOptions.FILE:
                    break;
                case InputOptions.DIRECTORY:
                    if (!Directory.Exists(Input)) {
                        throw new Exception("Input directory cannot be accessed!");
                    }
                    break;
                case InputOptions.RECURSIVE:
                    if (!Directory.Exists(Input)) {
                        throw new Exception("Input directory cannot be accessed!");
                    }
                    break;
            }
            if (!Directory.Exists(Output)) {
                throw new Exception("Output directory cannot be accessed!");
            }
        }

        internal void ValidateVideo() {
            Validate();
            if (Format == null) {
                throw new Exception("Container value was null!");
            }
            if (Video == null) {
                throw new Exception("Video options was null!");
            }
            if (Audio == null) {
                throw new Exception("Audio options are null and an option to cut audio is not set!");
            }
        }

        internal void ValidateAudio() {
            Validate();
            if (Format == null) {
                throw new Exception("Container value was null!");
            }
            if (Audio == null) {
                throw new Exception("Audio options was null!");
            }
        }
    }
}
