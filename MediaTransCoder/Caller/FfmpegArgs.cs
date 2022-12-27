using System.Text;

namespace MediaTransCoder.Backend {
    internal class FfmpegArgs {
        public string FfmpegPath { get; set; }
        //TODO: Obtain WorkingDirectory as common path of input and output path
        public string WorkingDirectory { get; set; }
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public bool OverrideExistingFiles { get; set; }
        public HardwareAcceleration Acceleration { get; set; }
        public LoggingLevel LoggingLevel { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? Audio { get; set; }
        public VideoOptions? Video { get; set; }

        internal string GetArgs() {
            StringBuilder sb = new StringBuilder();
            sb.Append("-hide_banner"); //Hide unused banner info
            sb.Append(" -loglevel " + EnumHelper.GetFfmpegLoggingLevel(LoggingLevel)); //Set logging level
            sb.Append(" -progress -");
            if (OverrideExistingFiles) {
                sb.Append(" -y"); //Override existing output files?
            }
            if (Acceleration == HardwareAcceleration.CPU) {
                sb.Append(" -threads " + Context.Get().Config.Hardware.CPUCores);
                //TODO: Mechanism of choosing codecs for GPU acceleration
            }
            sb.Append(" -i " + InputPath); //Single file path
            if(Video != null) {
                sb.Append(" -vcodec " + EnumHelper.GetCommand(Video.Codec));
                sb.Append(" -r " + Video.FPS);
                sb.Append(" -vf \"scale=" + EnumHelper.GetResolution(Video.Resolution) + "\"");
                sb.Append(" -b:v " + Video.BitRate + "k");
            }
            if(Audio != null) {
                sb.Append(" -acodec " + EnumHelper.GetCommand(Audio.Codec));
                sb.Append(" -b:a " + Audio.BitRate + "k");
                sb.Append(" -ar " + Audio.SamplingRate);
                sb.Append(" -ac " + Audio.AudioChannels);
            }
            if(Format != null)
                sb.Append(" -f " + EnumHelper.GetCommand(Format.Value));
            sb.Append(" " + OutputPath);
            return sb.ToString();
        }

        internal static FfmpegArgs Get(EndpointOptions options, string input, string output) {
            var result = new FfmpegArgs();
            result.InputPath = input;
            result.OutputPath = output;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            return result;
        }

        internal static FfmpegArgs Get(EndpointOptions options) {
            if(options.InputOption != InputOptions.FILE) {
                throw new Exception("Cannot convert Endpoint Options to FfmpegArgs!");
            }
            var result = new FfmpegArgs();
            result.InputPath = options.Input;
            result.OutputPath = options.Output;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            return result;
        }

        public FfmpegArgs() {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            LoggingLevel = LoggingLevel.WARNING;
            WorkingDirectory = Directory.GetCurrentDirectory();
            InputPath = string.Empty; 
            OutputPath = string.Empty;
            OverrideExistingFiles = true;
            Recursive = false;
            Acceleration = HardwareAcceleration.NONE;
        }

        public FfmpegArgs(string inputPath, string outputPath) {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            LoggingLevel = LoggingLevel.WARNING;
            if(!File.Exists(inputPath))
                throw new FileNotFoundException(inputPath);
            InputPath = inputPath;
            OutputPath = outputPath;
            //TODO: Shall be calculated from input and output
            WorkingDirectory = Directory.GetCurrentDirectory();
            Recursive = false;
            OverrideExistingFiles = true;
            Acceleration = HardwareAcceleration.NONE;
        }

        internal Dictionary<string, string> GetFilePathes() {
            var result = new Dictionary<string, string>();
            if (Recursive) { //Calc all file pathes

            } else {

            }
            return result;
        }
    }
}
