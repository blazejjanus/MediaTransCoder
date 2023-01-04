using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MediaTransCoder.Backend {
    internal class ImageConverter : AbstractConverter {
        public ImageConverter(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) : 
            base(args, callback, metadataCallback) {
        }

        public override int Convert() {
            if (File.Exists(args.Files.Output)) { //Skip already processed file?
                if (args.SkipExistingFiles) {
                    context.Display.Send("Skipping converted file!", MessageType.WARNING);
                    Progress = 100.0;
                    return 0;
                } else {
                    if (Logging.IsDebug) {
                        if (context.Display.GetBool("Shall remove existing file?")) {
                            context.Display.Send("Skipping converted file!", MessageType.WARNING);
                            Progress = 100.0;
                            return 0;
                        }
                    }
                }
            }
            throw new NotImplementedException();
        }

        protected override void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            throw new NotImplementedException();
        } 
    }
}
