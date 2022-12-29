using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    //internal delegate void OnProgressCallback(int progress);
    //internal delegate void OnMetadataUpdateCallback(FfmpegMetadata metadata);

    internal class FfmpegCaller : IDisposable {
        public bool IsRunning { get; private set; }
        public double Progress { 
            get {
                return Math.Round(((double)lastFrame / metadata.TotalNumberOfFrames) * 100, 1);
            } 
        }
        private readonly Context context;
        private readonly FfmpegArgs args;
        private readonly Process process;
        private bool wasStarted = false;
        internal readonly FfmpegMetadata metadata;
        private int lastFrame;
        OnProgressCallback? ProgressCallback;
        OnMetadataUpdateCallback? MetadataCallback;

        internal FfmpegCaller(FfmpegArgs args) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
            lastFrame = 0;
            IsRunning = false;
            metadata = new FfmpegMetadata();
            ProgressCallback = null;
            MetadataCallback = null;
        }

        internal FfmpegCaller(FfmpegArgs args, OnProgressCallback? callback, OnMetadataUpdateCallback? metadataCallback) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
            lastFrame = 0;
            IsRunning = false;
            metadata = new FfmpegMetadata();
            ProgressCallback = callback;
            MetadataCallback = metadataCallback;
        }

        public int Run() {
            Logging.Debug("FFmpeg command:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\n");
            if (Logging.IsDebug) {
                if (File.Exists(args.Files.Output)) { //Skip already processed file?
                    if (context.Display.GetBool("Shall remove existing file?")) {
                        context.Display.Send("Skipping convertedfile!", MessageType.WARNING);
                        return 0;
                    }
                }
            }
            process.OutputDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            metadata.Read(args.Files.Input);
            if(MetadataCallback!= null) {
                MetadataCallback(metadata);
            }
            string? outputDirPath = Path.GetDirectoryName(args?.Files.Output);
            if(outputDirPath != null) {
                if(!Directory.Exists(outputDirPath)) { 
                    Directory.CreateDirectory(outputDirPath);
                }
            }
            process.Start();
            wasStarted = true;
            IsRunning = true;
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            IsRunning = false;
            return process.ExitCode;
        }

        internal bool Test() {
            try {
                context.Display.Send("Starting process:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments, MessageType.WARNING);
                if(File.Exists(args.Files.Output)) {
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
                    if(file.Length < 1000) {
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
        
        private void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            if(outLine.Data != null) {
                context.Display.Send(outLine.Data);
                if (outLine.Data.Contains("frame=")) {
                    lastFrame = Int32.Parse(outLine.Data.Split("=")[1].Trim());
                    if(ProgressCallback != null) {
                        ProgressCallback(lastFrame);
                    }
                }
            }
        }

        private void OnProcessExit(object sender, EventArgs e) {
            IsRunning = false;
        }

        private Process PrepeareProcess() {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.FileName = args.FfmpegPath;
            proc.StartInfo.Arguments = args.GetArgs();
            process.Exited += new EventHandler(OnProcessExit);
            return proc;
        }

        public void Dispose() {
            if(process != null && wasStarted) {
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
