using System.Text;

namespace MediaTransCoder.Backend
{
    internal class FfmpegArgs {
        internal string FfmpegPath { get; set; }
        //TODO: Obtain WorkingDirectory as common path of input and output path
        internal string WorkingDirectory { get; set; }
        internal string InputPath { get; set; }
        internal string OutputPath { get; set; }
        internal bool OverrideExistingFiles { get; set; }
        internal LoggingLevel LoggingLevel { get; set; }
        internal ContainerFormat? Format { get; set; }
        internal AudioCodecs? AudioCodec { get; set; }
        internal VideoCodecs? VideoCodec { get; set; }

        internal string GetArgs() {
            StringBuilder sb = new StringBuilder();
            sb.Append(" -hide_banner"); //Hide unused banner info
            sb.Append(" -loglevel " + EnumHelper.GetFfmpegLoggingLevel(LoggingLevel)); //Set logging level
            if (OverrideExistingFiles)
                sb.Append(" -y"); //Override existing output files?
            sb.Append(" -threads 16");
            sb.Append(" -i " + InputPath); //Single file path
            if(AudioCodec != null)
                sb.Append(" -acodec " + EnumHelper.GetCommand(AudioCodec.Value));
            if(VideoCodec != null)
                sb.Append(" -vcodec " + EnumHelper.GetCommand(VideoCodec.Value));
            if(Format != null)
                sb.Append(" -f " + EnumHelper.GetCommand(Format.Value));
            sb.Append(" " + OutputPath);
            return sb.ToString();
        }
        internal FfmpegArgs() {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            LoggingLevel = LoggingLevel.WARNING;
            WorkingDirectory = Directory.GetCurrentDirectory();
            InputPath = string.Empty; 
            OutputPath = string.Empty;
        }
        internal FfmpegArgs(string inputPath, string outputPath) {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            LoggingLevel = LoggingLevel.WARNING;
            if(!File.Exists(inputPath))
                throw new FileNotFoundException(inputPath);
            InputPath = inputPath;
            OutputPath = outputPath;
            //TODO: Shall be calculated from input and output
            WorkingDirectory = Directory.GetCurrentDirectory();
        }
    }
}
