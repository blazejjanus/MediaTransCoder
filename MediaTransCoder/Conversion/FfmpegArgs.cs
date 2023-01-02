using System.Text;

namespace MediaTransCoder.Backend
{
    internal class FfmpegArgs {
        public string FfmpegPath { get; set; }
        public FileOption Files { get; set; }
        public bool OverrideExistingFiles { get; set; }
        public bool AudioOnly { get; set; }
        public HardwareAcceleration Acceleration { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? Audio { get; set; }
        public VideoOptions? Video { get; set; }

        public FfmpegArgs() {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            Files = new FileOption();
            OverrideExistingFiles = true;
            AudioOnly = false;
            Acceleration = HardwareAcceleration.NONE;
        }

        internal static FfmpegArgs Get(EndpointOptions options, string input, string output) {
            var result = new FfmpegArgs();
            result.Files.Input = input;
            result.Files.Output = output;
            result.AudioOnly = options.AudioOnly;
            result.OverrideExistingFiles = options.OverrideExistingFiles;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            return result;
        }

        internal static FfmpegArgs Get(EndpointOptions options) {
            if (options.InputOption != InputOptions.FILE) {
                throw new Exception("Cannot convert Endpoint Options to FfmpegArgs!");
            }
            var result = new FfmpegArgs();
            result.Files.Input = options.Input;
            result.Files.Output = options.Output;
            result.AudioOnly = options.AudioOnly;
            result.OverrideExistingFiles = options.OverrideExistingFiles;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            return result;
        }

        internal string GetArgs() {
            StringBuilder sb = new StringBuilder();
            sb.Append("-hide_banner"); //Hide unused banner info
            sb.Append(" -loglevel " + Logging.LoggingLevel); //Set logging level
            sb.Append(" -progress -");
            if (OverrideExistingFiles) {
                sb.Append(" -y"); //Override existing output files?
            }
            if (Acceleration == HardwareAcceleration.CPU) {
                sb.Append(" -threads " + Context.Get().Config.Hardware.CPUCores);
                //TODO: Mechanism of choosing codecs for GPU acceleration
            }
            sb.Append(" -i \"" + Files.Input + "\""); //Single file path
            if(Video != null) {
                sb.Append(" -vcodec " + EnumHelper.GetName(Video.Codec));
                sb.Append(" -r " + Video.FPS);
                sb.Append(" -vf \"scale=" + EnumHelper.GetResolution(Video.Resolution) + "\"");
                sb.Append(" -b:v " + Video.BitRate + "k");
            }
            if(Audio != null) {
                sb.Append(" -acodec " + EnumHelper.GetName(Audio.Codec));
                sb.Append(" -b:a " + Audio.BitRate + "k");
                sb.Append(" -ar " + Audio.SamplingRate);
                sb.Append(" -ac " + Audio.AudioChannels);
            }
            if(Format != null) {
                sb.Append(" -f " + EnumHelper.GetName(Format.Value));
            }
            sb.Append(" \"" + Files.Output + "\"");
            return sb.ToString();
        }

        public void GenerateOutputFileName() {
            string name = Path.GetFileNameWithoutExtension(Files.Input);
            string? containerExtension = null;
            string? codecExtension = null;
            if(Format != null) {
                containerExtension = EnumHelper.GetFileExtension(Format.Value, AudioOnly);
            }
            if(Video != null) {
                codecExtension = EnumHelper.GetFileExtension(Video.Codec);
            } else {
                if(Audio != null) {
                    codecExtension = EnumHelper.GetFileExtension(Audio.Codec);
                }
            }
            if(codecExtension != null) {
                name += codecExtension;
            } else {
                if(containerExtension != null) {
                    name += containerExtension;
                }
            }
            if (Files.Output.EndsWith("..")) {
                Files.Output = Files.Output.Split("..").First();
            }
            Files.Output = Path.Combine(Files.Output, name);
        }
    }
}
