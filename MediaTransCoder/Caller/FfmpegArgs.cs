using System.ComponentModel;
using System.Text;

namespace MediaTransCoder.Backend {
    public class FfmpegArgs {
        public string FfmpegPath { get; set; }
        //TODO: Obtain WorkingDirectory as common path of input and output path
        public string WorkingDirectory { get; set; }
        public string InputPath { get; set; }
        public bool Recursive { get; set; }
        public string OutputPath { get; set; }
        public bool OverrideExistingFiles { get; set; }
        public LoggingLevel LoggingLevel { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? AudioOptions { get; set; }
        public VideoOptions? VideoOptions { get; set; }

        internal string GetArgs() {
            StringBuilder sb = new StringBuilder();
            sb.Append("-hide_banner"); //Hide unused banner info
            sb.Append(" -loglevel " + EnumHelper.GetFfmpegLoggingLevel(LoggingLevel)); //Set logging level
            sb.Append(" -progress");
            if (OverrideExistingFiles)
                sb.Append(" -y"); //Override existing output files?
           // sb.Append(" -threads " + 16);
            sb.Append(" -i " + InputPath); //Single file path
            if(AudioOptions != null) {
                sb.Append(" -acodec " + EnumHelper.GetCommand(AudioOptions.Codec));
            } 
            if(VideoOptions != null) {
                sb.Append(" -vcodec " + EnumHelper.GetCommand(VideoOptions.Codec));
                sb.Append(" -r " + VideoOptions.FPS);
                sb.Append(" -vf \"scale=" + EnumHelper.GetResolution(VideoOptions.Resolution) + "\"");
                sb.Append(" -b:v " + VideoOptions.GetFormatedBitRate());
            }
            if(Format != null)
                sb.Append(" -f " + EnumHelper.GetCommand(Format.Value));
            sb.Append(" " + OutputPath);
            return sb.ToString();
        }

        public FfmpegArgs() {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            LoggingLevel = LoggingLevel.WARNING;
            WorkingDirectory = Directory.GetCurrentDirectory();
            InputPath = string.Empty; 
            OutputPath = string.Empty;
            Recursive = false;
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
        }

        private void Validate() {
            if(Recursive) {
                if (!Directory.Exists(InputPath)) {
                    throw new Exception("Input directory cannot be accessed!");
                }
            } else {
                if (!File.Exists(InputPath)) {
                    throw new Exception("Input file cannot be accessed!");
                }
            }
        }

        internal void ValidateVideo() {
            Validate();
            if(Format == null) {
                throw new Exception("Container value was null!");
            }
            if(VideoOptions == null) {
                throw new Exception("Video options was null!");
            }
        }
    }
}
