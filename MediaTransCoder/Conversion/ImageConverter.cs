using System.Diagnostics;

namespace MediaTransCoder.Backend {
    internal class ImageConverter : AbstractConverter {
        public ImageConverter(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) : 
            base(args, callback, metadataCallback) {

        }

        public override int Convert() {
            throw new NotImplementedException();
        }

        protected override void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            throw new NotImplementedException();
        }
    }
}
