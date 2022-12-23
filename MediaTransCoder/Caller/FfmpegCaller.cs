using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal class FfmpegCaller : IDisposable {
        private Context context;
        private FfmpegArgs args;
        private Process process;
        private bool started;
        private string progressOutput;
        private int progressOutputIndex;
        private FfmpegVideoDetection metadata;

        internal FfmpegCaller(FfmpegArgs args) {
            context = Context.Get();
            this.args = args;
            started = false;
            process = PrepeareProcess();
            progressOutput = string.Empty;
            progressOutputIndex = 0;
            metadata = new FfmpegVideoDetection();
        }

        public double Progress {
            get {
                return 0;
            }
        }

        public void RunAsync() {
            context.Display.Send(process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\n\n");
            process.OutputDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(FfmpegOutputHandler);
            metadata.Read(args.InputPath);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            context.Display.Send("\n\nFFmpeg output after process exited: \n" + progressOutput);
        }

        public void Run() {
            /*
            var timer = new System.Timers.Timer(500);
            timer.Elapsed += (sender, e) => {
                double progress = GetCurrentProgress();
                context.Display.UpdateProgress(progress);
            };
            timer.Start();
            */
            //timer.Stop();
            context.Display.UpdateProgress(100);
            process.Start();
            process.WaitForExit();
            context.Display.Send("\n\nFFmpeg output after process exited: \n" + progressOutput);
        }

        internal bool Test() {
            try {
                context.Display.Send("Starting process:\n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments, MessageType.WARNING);
                if(File.Exists(args.OutputPath)) {
                    context.Display.Send("File " + args.OutputPath + " already exists!", MessageType.ERROR);
                    context.Display.Send("Skipping.", MessageType.SUCCESS);
                    return true;
                }
                started = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                context.Display.Send(output);
                if (File.Exists(args.OutputPath)) {
                    FileInfo file = new FileInfo(args.OutputPath);
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

        private void FfmpegOutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            if(outLine.Data != null) {
                progressOutputIndex++;
                context.Display.Send(progressOutputIndex + ":\n" + outLine.Data);
                progressOutput += outLine.Data;
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
                if (started) {
                    process.Kill();
                }
                process.Dispose();
            }
        }
    }
}
