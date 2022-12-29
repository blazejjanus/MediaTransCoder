using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal delegate void OnProgressCallback(int progress);
    internal delegate void OnMetadataUpdateCallback(FfmpegMetadata metadata);

    internal abstract class AbstractConverter : IDisposable {
        public double Progress { get; protected set; }
        public bool WasStarted { get; protected set; } = false;
        public bool IsRunning { get; protected set; } = false;
        protected readonly Context context;
        protected readonly FfmpegArgs args;
        protected readonly Process process;
        protected FfmpegMetadata metadata;
        protected OnProgressCallback? ProgressCallback;
        protected OnMetadataUpdateCallback? MetadataCallback;

        public AbstractConverter(FfmpegArgs args, OnProgressCallback? progressCallback, OnMetadataUpdateCallback? metadataCallback) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
            ProgressCallback = progressCallback;
            MetadataCallback = metadataCallback;
            metadata = new FfmpegMetadata();
        }

        public abstract int Convert();

        protected abstract void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine);

        internal abstract bool Test();

        protected void OnProcessExit(object sender, EventArgs e) {
            IsRunning = false;
            if(process.ExitCode != 0) {
                if (File.Exists(args.Files.Output)) {
                    File.Delete(args.Files.Output); //Remove uncompleted conversion file
                }
                throw new Exception("Fmmpeg exited with status code: " + process.ExitCode);
            }
        }

        protected void ReadMetadata() {
            if (args.AudioOnly) {
                metadata.ReadAudio(args.Files.Input);
            } else {
                metadata.ReadVideo(args.Files.Input);
            }
            if (MetadataCallback != null) {
                MetadataCallback(metadata);
            }
        }

        protected void CheckOutputDirectory() {
            string? outputDirPath = Path.GetDirectoryName(args?.Files.Output);
            if (outputDirPath != null) {
                if (!Directory.Exists(outputDirPath)) {
                    Directory.CreateDirectory(outputDirPath);
                    Logging.Debug("Created directory: " + outputDirPath);
                }
            }
        }

        protected int StartProcess() {
            WasStarted = true;
            IsRunning = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            return process.ExitCode;
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

        private Process PrepeareProcess() {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.FileName = args.FfmpegPath;
            proc.StartInfo.Arguments = args.GetArgs();
            proc.Exited += new EventHandler(OnProcessExit);
            proc.OutputDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            proc.ErrorDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            return proc;
        }
    }
}
