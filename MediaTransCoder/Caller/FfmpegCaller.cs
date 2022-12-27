using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal class FfmpegCaller : IDisposable {
        public double Progress { 
            get {
                return Math.Round((lastFrame / (metadata.TotalNumberOfFrames * metadata.Multiplayer)) * 100, 1);
            } 
        }
        private readonly Context context;
        private readonly FfmpegArgs args;
        private readonly Process process;
        private readonly FfmpegVideoDetection metadata;
        private int lastFrame;

        internal FfmpegCaller(FfmpegArgs args) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
            lastFrame = 0;
            metadata = new FfmpegVideoDetection();
        }

        public void Run() {
            if((int)context.Config.Logging.LoggingLevel > 2) {
                context.Display.Send("FFmpeg command:\n" + 
                    process.StartInfo.FileName + " " + 
                    process.StartInfo.Arguments + "\n\n");
            }
            process.OutputDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            metadata.Read(args.InputPath);
            metadata.CalcMultiplayer(args?.Video?.FPS ?? metadata.FPS);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        internal bool Test() {
            try {
                context.Display.Send("Starting process:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments, MessageType.WARNING);
                if(File.Exists(args.OutputPath)) {
                    context.Display.Send("File " + args.OutputPath + " already exists!", MessageType.ERROR);
                    context.Display.Send("Skipping.", MessageType.SUCCESS);
                    return true;
                }
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                context.Display.Send(output);
                if (File.Exists(args.OutputPath)) {
                    var file = new FileInfo(args.OutputPath);
                    context.Display.Send("\tCreated file size: " + file.Length, MessageType.SUCCESS);
                    if(file.Length < 1000) {
                        context.Display.Send("\t\tDeleting file!", MessageType.ERROR);
                        File.Delete(args.OutputPath);
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
        private bool first = true;
        private void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            if(outLine.Data != null) {
                if (outLine.Data.Contains("frame=")) {
                    lastFrame = Int32.Parse(outLine.Data.Split("=")[1].Trim());
                    if (first) {
                        context.Display.Send(metadata.TotalNumberOfFrames + "  " + metadata.Multiplayer, MessageType.ERROR);
                        first = false;
                    }
                    context.Display.Send(lastFrame + " / " + metadata.TotalNumberOfFrames * metadata.Multiplayer, MessageType.WARNING);
                    context.Display.Send(Progress + "%");
                }
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
            return proc;
        }

        public void Dispose() {
            if(process != null) {
                if (!process.HasExited) {
                    process.Kill();
                    if (File.Exists(args.OutputPath)) {
                        File.Delete(args.OutputPath); //Remove uncompleted conversion file
                    }
                }
                process.Dispose();
            }
        }
    }
}
