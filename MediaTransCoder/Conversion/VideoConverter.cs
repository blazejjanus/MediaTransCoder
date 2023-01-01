using System.Diagnostics;

namespace MediaTransCoder.Backend {
    internal class VideoConverter : AbstractConverter {
        internal int LastFrame { get; set; }

        public VideoConverter(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) : 
            base(args, callback, metadataCallback) {
        }

        public override int Convert() {
            Logging.Debug("FFmpeg command:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\n");
            if (Logging.IsDebug) {
                if (File.Exists(args.Files.Output)) { //Skip already processed file?
                    if(context.Display.GetBool("Shall remove existing file?")) {
                        context.Display.Send("Skipping convertedfile!", MessageType.WARNING);
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
                context.Display.Send(outLine.Data);
                if (outLine.Data.Contains("frame=")) {
                    LastFrame = Int32.Parse(outLine.Data.Split("=")[1].Trim());
                    Progress = Math.Round((double)LastFrame / metadata.TotalNumberOfFrames * 100, 1);
                    if (ProgressCallback != null) {
                        ProgressCallback(LastFrame);
                    }
                }
            }
        }

        internal override bool Test() {
            try {
                context.Display.Send("Starting process:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments, MessageType.WARNING);
                if (File.Exists(args.Files.Output)) {
                    context.Display.Send("File " + args.Files.Output + " already exists!", MessageType.ERROR);
                    context.Display.Send("Skipping.", MessageType.SUCCESS);
                    return true;
                }
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                context.Display.Send(output);
                if (File.Exists(args.Files.Output)) {
                    var file = new FileInfo(args.Files.Output);
                    context.Display.Send("\tCreated file size: " + file.Length, MessageType.SUCCESS);
                    if (file.Length < 1000) {
                        context.Display.Send("\t\tDeleting file!", MessageType.ERROR);
                        File.Delete(args.Files.Output);
                        return false;
                    }
                }
                if (process.ExitCode != 0) {
                    return false;
                } else {
                    return true;
                }
            } catch (Exception exc) {
                context.Display.Send(exc.Message, MessageType.ERROR);
                Debug.WriteLine(exc.ToString());
                return false;
            }
        }
    }
}
