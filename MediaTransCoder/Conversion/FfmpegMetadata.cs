using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
[assembly: InternalsVisibleTo("MediaTransCoder.UnitTests")]
namespace MediaTransCoder.Backend {
    internal class FfmpegMetadata: IDisposable {
        public TimeParser Duration { get; private set; }
        public int FPS { get; private set; } = 0;
        public int TotalNumberOfFrames {
            get {
                return (Duration?.TotalSeconds ?? 0) * FPS;
            }
        }
        private Context context;

        public FfmpegMetadata() {
            Duration = new TimeParser();
            context = Context.Get();
        }

        public void ReadVideo(string filePath) {
            var processOutput = RunProcess(filePath);
            ParseDuration(processOutput);
            ParseFPS(processOutput);
        }

        public void ReadAudio(string filePath) {
            var processOutput = RunProcess(filePath);
            ParseDuration(processOutput);
        }

        private string RunProcess(string filePath) {
            string processOutput = string.Empty;
            var process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "ffmpeg",
                    WorkingDirectory = Path.GetDirectoryName(filePath),
                    Arguments = "-hide_banner -vstats -i \"" + Path.GetFileName(filePath) + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };
            Logging.Debug("Metadata ffmpeg call: \n" + process.StartInfo.FileName + " " + process.StartInfo.Arguments);
            process.Start();
            processOutput += process.StandardOutput.ReadToEnd();
            processOutput += process.StandardError.ReadToEnd();
            process.WaitForExit();
            return processOutput;
        }

        private void ParseDuration(string output) {
            string duration = string.Empty;
            if (output.Contains("Duration:")) {
                output = output.Substring(output.IndexOf("Duration:"));
                int Pos1 = output.IndexOf("Duration:") + "Duration:".Length;
                int Pos2 = output.IndexOf(",");
                duration = output.Substring(Pos1, Pos2 - Pos1).Trim();
            }
            Duration = new TimeParser(duration);
        }

        private void ParseFPS(string output) {
            string fps = string.Empty;
            if (output.Contains("Video:")) {
                int Pos1 = output.IndexOf("Video:") + "Video:".Length;
                if (output.Contains("fps")) {
                    int Pos2 = output.IndexOf("fps") + "fps".Length;
                    string temp = output.Substring(Pos1, Pos2 - Pos1).Trim();
                    string[] lines = temp.Split(',');
                    foreach (var line in lines) {
                        if (line.Contains("fps")) {
                            fps = line.Split("fps").First().Trim();
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(fps)) {
                FPS = NumberParser.ParseDoubleStringToInt(fps);
            } else {
                throw new Exception("Cannot parse fps!");
            }
        }

        /// <summary>
        /// Parse video file metadata
        /// </summary>
        /// <param name="output">Ffmpeg video file metadata</param>
        private void ParseFfmpegOutput(string output) {
            Logging.Debug("Metadata output: \n" + output);
            string duration = string.Empty;
            string fps = string.Empty;
            //Parse duration
            if (output.Contains("Duration:")) {
                output = output.Substring(output.IndexOf("Duration:"));
                int Pos1 = output.IndexOf("Duration:") + "Duration:".Length;
                int Pos2 = output.IndexOf(",");
                duration = output.Substring(Pos1, Pos2 - Pos1).Trim();
            }
            var parser = new TimeParser(duration);
            //Parse fps
            if (output.Contains("Video:")) {
                int Pos1 = output.IndexOf("Video:") + "Video:".Length;
                if (output.Contains("fps")) {
                    int Pos2 = output.IndexOf("fps") + "fps".Length;
                    string temp = output.Substring(Pos1, Pos2 - Pos1).Trim();
                    string[] lines = temp.Split(',');
                    foreach (var line in lines) {
                        if (line.Contains("fps")) {
                            fps = line.Split("fps").First().Trim();
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(fps)) {
                FPS = NumberParser.ParseDoubleStringToInt(fps);
            } else {
                FPS = 1;
            }
            //Calc needed data
            //TotalNumberOfFrames = parser.TotalSeconds * FPS;
        }

        public void Dispose() {
            
        }
    }
}
