using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal delegate void OnProgressCallback(int progress);
    internal delegate void OnMetadataUpdateCallback(FfmpegMetadata metadata);

    internal abstract class AbstractConverter : IDisposable {
        public double Progress { get; protected set; }
        internal bool WasStarted { get; private set; }
        internal bool IsRunning { get; private set; }
        private readonly Context context;
        private readonly FfmpegArgs args;
        private readonly Process process;
        private OnProgressCallback? ProgressCallback;
        private OnMetadataUpdateCallback? MetadataCallback;

        internal AbstractConverter(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
        }

        private Process PrepeareProcess() {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.FileName = args.FfmpegPath;
            proc.StartInfo.Arguments = args.GetArgs();
            return proc;
        }

        public abstract void Convert(FfmpegArgs args);

        protected abstract void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine);

        internal abstract bool Test();

        private void OnProcessExit(object sender, EventArgs e) {
            IsRunning = false;
            if(process.ExitCode != 0) {
                if (File.Exists(args.Files.Output)) {
                    File.Delete(args.Files.Output); //Remove uncompleted conversion file
                }
                throw new Exception("Fmmpeg exited with status code: " + process.ExitCode);
            }
        }

        public void Dispose() {
            if (process != null && WasStarted) {
                process.Refresh();
                if (!process.HasExited) {
                    process.Kill();
                    if (File.Exists(args.Files.Output)) {
                        File.Delete(args.Files.Output); //Remove uncompleted conversion file
                    }
                }
                process.Dispose();
            }
        }
    }
}
