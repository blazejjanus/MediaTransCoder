using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
[assembly: InternalsVisibleTo("MediaTransCoder.UnitTests")]
namespace MediaTransCoder.Backend {
    internal delegate void OnProgressCallback(int progress);
    internal delegate void OnMetadataUpdateCallback(FfmpegMetadata metadata);

    internal abstract class AbstractConverter : IDisposable {
        public double Progress { get; protected set; }
        public bool WasStarted { get; protected set; } = false;
        public bool IsRunning { get; protected set; } = false;
        public Measurer Measures { get; protected set; }
        protected readonly Context context;
        protected readonly FfmpegArgs args;
        protected readonly Process process;
        protected FfmpegMetadata metadata;
        protected OnProgressCallback? ProgressCallback;
        protected OnMetadataUpdateCallback? MetadataCallback;
        protected string errorString = string.Empty;

        public string OutputFile {
            get {
                return args.Files.Output;
            }
        }

        public string InputFile {
            get {
                return args.Files.Input;
            }
        }

        public AbstractConverter(FfmpegArgs args, OnProgressCallback? progressCallback, OnMetadataUpdateCallback? metadataCallback) {
            context = Context.Get();
            this.args = args;
            process = PrepareProcess();
            ProgressCallback = progressCallback;
            MetadataCallback = metadataCallback;
            metadata = new FfmpegMetadata();
            Measures = new Measurer(args.Files);
        }

        public abstract int Convert();

        protected void DebugInfo() {
            if (context.IsDebug ?? false) {
                Logging.Debug("FFMPEG args: " + process.StartInfo.Arguments);
            }
        }

        protected abstract void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine);
        protected void FfmpegErrorHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            errorString += outLine.Data;
        }

        internal bool Test() {
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

        protected void OnProcessExit(object sender, EventArgs e) {
            IsRunning = false;
            if(process.ExitCode != 0) {
                if (File.Exists(args.Files.Output)) {
                    File.Delete(args.Files.Output); //Remove uncompleted conversion file
                }
                throw new Exception("Fmmpeg exited with status code: " + process.ExitCode, new Exception(errorString));
            }
        }

        protected void ReadMetadata() {
            //REFACTOR: One method to check if it's audio video or image
            if (args.Image != null) {
                metadata.ReadImage(args.Files.Input);
            } else {
                if (args.AudioOnly) {
                    metadata.ReadAudio(args.Files.Input);
                } else {
                    metadata.ReadVideo(args.Files.Input);
                }
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
            DebugInfo();
            WasStarted = true;
            IsRunning = true;
            Measures.StartMeasure();
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            if(process.ExitCode == 0) {
                Measures.EndMeasure();
                context.Display.ShowResults(Measures);
            }
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

        private Process PrepareProcess() {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.FileName = args.FfmpegPath;
            proc.StartInfo.Arguments = args.GetArgs();
            proc.Exited += new EventHandler(OnProcessExit);
            proc.OutputDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            proc.ErrorDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            proc.ErrorDataReceived += new DataReceivedEventHandler(FfmpegErrorHandler);
            return proc;
        }
    }
}
