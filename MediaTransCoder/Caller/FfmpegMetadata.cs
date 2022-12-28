using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal class FfmpegMetadata: IDisposable {
        public int FPS { get; private set; }
        public int TotalNumberOfFrames { get; private set; }
        public double Multiplayer { get; private set; }

        public void Read(string filePath) {
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
            if (Context.Get().Config.Environment == EnvironmentType.Development) {
                Context.Get().Display.Send("Metadata ffmpeg call: \n" + process.StartInfo.FileName + " " 
                    + process.StartInfo.Arguments, MessageType.WARNING);
            }
            process.Start();
            processOutput += process.StandardOutput.ReadToEnd();
            processOutput += process.StandardError.ReadToEnd();
            process.WaitForExit();
            ParseFfmpegOutput(processOutput);
        }

        public void CalcMultiplayer(int outputFPS) {
            Multiplayer = outputFPS / FPS;
        }

        /// <summary>
        /// Parse video file metadata
        /// </summary>
        /// <param name="output">Ffmpeg video file metadata</param>
        private void ParseFfmpegOutput(string output) {
            if(Context.Get().Config.Environment == EnvironmentType.Development) {
                Context.Get().Display.Send("Metadata output: \n" + output, MessageType.WARNING);
            }
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
            TotalNumberOfFrames = parser.TotalSeconds * FPS;
            Multiplayer = 1; //Assume the FPS won't change.
        }

        public void Dispose() {
            
        }
    }
}
