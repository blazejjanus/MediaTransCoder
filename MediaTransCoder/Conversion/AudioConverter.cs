using System.Diagnostics;

namespace MediaTransCoder.Backend {
    internal class AudioConverter : AbstractConverter {
        internal int LastTimeStamp { get; set; }

        public AudioConverter(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) : 
            base(args, callback, metadataCallback) {
        }

        public override int Convert() {
            Logging.Debug("FFmpeg command:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\n");
            if (Logging.IsDebug) {
                if (File.Exists(args.Files.Output)) { //Skip already processed file?
                    if (context.Display.GetBool("Shall remove existing file?")) {
                        context.Display.Send("Skipping converted file!", MessageType.WARNING);
                        Progress = 100.0;
                        return 0;
                    }
                }
            }
            ReadMetadata();
            CheckOutputDirectory();
            return StartProcess();
        }

        protected override void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            if (outLine.Data != null) {
                Logging.Debug(outLine.Data);
                if (outLine.Data.Contains("frame=")) {
                    LastTimeStamp = Int32.Parse(outLine.Data.Split("=")[1].Trim());
                    Progress = Math.Round((double)LastTimeStamp / metadata.Duration.TotalMiliseconds * 100, 1);
                    if (ProgressCallback != null) {
                        ProgressCallback(LastTimeStamp);
                    }
                }
            }
        }
    }
}
