using MediaTransCoder.Shared;
using System.Text;

namespace MediaTransCoder.Backend {
    public class EndpointOptions {
        public bool OverrideExistingFiles { get; set; }
        public bool AllowDirectoryCreation { get; set; }
        public bool SkipExistingFiles { get; set; }
        public bool AudioOnly { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string? OutputFileName { get; set; }
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

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Endpoint options:");
            sb.AppendLine("\tInput:           " + InputOption);
            sb.AppendLine("\tInputPath:       " + Input);
            sb.AppendLine("\tOutputPath:      " + Output);
            sb.AppendLine("\tAcceleration:    " + Acceleration);
            if (Format != null) {
                sb.AppendLine("\tContainer:       " + Format);
            }
            if(Audio != null) {
                sb.AppendLine(AddTabulation(Audio.ToString()));
            }
            if (Video != null) {
                sb.AppendLine(AddTabulation(Video.ToString()));
            }
            if (Image != null) {
                sb.AppendLine(AddTabulation(Image.ToString()));
            }
            sb.AppendLine("\tAudioOnly:       " + AudioOnly);
            sb.AppendLine("\tOverrideFiles:   " + OverrideExistingFiles);
            sb.AppendLine("\tSkipExisting:    " + SkipExistingFiles);
            sb.AppendLine("\tDirCreation:     " + AllowDirectoryCreation);
            return sb.ToString();
        }

        private string AddTabulation(string str) {
            var sb = new StringBuilder();
            var lines = str.Split('\n');
            foreach(var line in lines) {
                sb.AppendLine("\t" + line);
            }
            return sb.ToString().TrimEnd();
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
                    SamplingRate = 48000
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
                    SamplingRate = 48000
                }
            };
        }

        public static EndpointOptions GetSampleAudioOptions() {
            var testEnv = TestingEnvironment.Get();
            return GetSampleAudioOptions(testEnv.Audio.Input, testEnv.Audio.Output);
        }

        public static EndpointOptions GetSampleImageOptions(string input, string output) {
            return new EndpointOptions() {
                Input = input,
                Output = output,
                InputOption = InputOptions.FILE,
                AudioOnly = false,
                Format = null,
                Video = null,
                Audio = null,
                Image = new ImageOptions() {
                    //Format = ImageFormat.JPG,
                    Format = ImageFormat.PNG,
                    Size = new System.Numerics.Vector2(1920, 1080),
                    //CompressionLevel = 5,
                    PixelFormat = PixelFormats.RGB24,
                    Brightness = null,
                    Contrast = null,
                    Saturation = null,
                    Effect = null,
                }
            };
        }

        public static EndpointOptions GetSampleImageOptions() {
            var testEnv = TestingEnvironment.Get();
            return GetSampleImageOptions(testEnv.Image.Input, testEnv.Image.Output);
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
                if (AllowDirectoryCreation) {
                    Logging.Debug("Creating directory: " + Output);
                    Directory.CreateDirectory(Output);
                } else {
                    throw new Exception("Output directory cannot be accessed!");
                }
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
