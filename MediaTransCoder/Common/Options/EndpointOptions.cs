using MediaTransCoder.Shared;

namespace MediaTransCoder.Backend {
    public class EndpointOptions {
        public bool OverrideExistingFiles { get; set; }
        public bool AllowDirectoryCreation { get; set; }
        public bool SkipExistingFiles { get; set; }
        public bool AudioOnly { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public InputOptions InputOption { get; set; }
        public HardwareAcceleration Acceleration { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? Audio { get; set; }
        public VideoOptions? Video { get; set; }
        public ImageOptions? Image { get; set; }

        public EndpointOptions() {
            OverrideExistingFiles = true;
            AudioOnly = false;
            AllowDirectoryCreation = true;
            SkipExistingFiles = true;
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
                if(AllowDirectoryCreation) {
                    Logging.Debug("Creating directory: " + Output);
                    Directory.CreateDirectory(Output);
                } else {
                    throw new Exception("Output directory cannot be accessed!");
                }
            }
        }

        public static EndpointOptions GetSampleVideoOptions(string input, string output) {
            return new EndpointOptions() {
                Input = input,
                Output = output,
                InputOption = InputOptions.FILE,
                AudioOnly = false,
                Format = ContainerFormat.matroska,
                Video = new VideoOptions() {
                    Codec = VideoCodecs.hevc,
                    Resolution = Resolutions.r1080p,
                    FPS = 60,
                    BitRate = 35000
                },
                Audio = new AudioOptions() {
                    Codec = AudioCodecs.mp3,
                    BitRate = 128,
                    AudioChannels = 1,
                    SamplingRate = 44100
                }
            };
        }

        public static EndpointOptions GetSampleVideoOptions() {
            var testEnv = TestingEnvironment.Get();
            return GetSampleVideoOptions(testEnv.Video.Input, testEnv.Video.Output);
        }

        public static EndpointOptions GetSampleAudioOptions(string input, string output) {
            return new EndpointOptions() {
                Input = input,
                Output = output,
                InputOption = InputOptions.FILE,
                AudioOnly = true,
                Format = ContainerFormat.avi,
                Audio = new AudioOptions() {
                    Codec = AudioCodecs.mp3,
                    BitRate = 128,
                    AudioChannels = 2,
                    SamplingRate = 44100
                }
            };
        }

        public static EndpointOptions GetSampleAudioOptions() {
            var testEnv = TestingEnvironment.Get();
            return GetSampleAudioOptions(testEnv.Audio.Input, testEnv.Audio.Output);
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

        internal void ValidateImage() {
            Validate();
            if(Image == null) {
                throw new Exception("Image options was null!");
            } else {
                if(Image.Size.X < 1 || Image.Size.Y < 1) {
                    throw new Exception("Image size must be specified!");
                }
            }
        }
    }
}
