using System.Diagnostics;

namespace MediaTransCoder.Backend
{
    internal class FfmpegCaller : IDisposable {
        private Context context;
        private FfmpegArgs args;
        private Process process;
        internal FfmpegCaller(FfmpegArgs args) {
            context = Context.Get();
            this.args = args;
            process = PrepeareProcess();
        }
        internal bool Running {
            get {
                return !process.HasExited;
            }
        }
        internal int? StatusCode {
            get {
                if(process.HasExited)
                    return process.ExitCode;
                return null;
            }
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
                context.Display.Send(output);
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
            process.Kill();
        }
    }
}
