using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal class FfmpegVideoDetection: IDisposable {
        public int FPS { get; private set; }
        public int TotalNumberOfFrames { get; private set; }
        public double Multiplayer { get; private set; }

        public void Read(string filePath) {
            string processOutput = string.Empty;
            var process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "ffmpeg",
                    WorkingDirectory = Path.GetDirectoryName(filePath),
                    Arguments = "-hide_banner -vstats -i " + Path.GetFileName(filePath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };
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
            FPS = NumberParser.ParseDoubleStringToInt(fps);
            //Calc needed data
            TotalNumberOfFrames = parser.TotalSeconds * FPS;
            Multiplayer = 1; //Assume the FPS won't change.
        }

        public void Dispose() {
            
        }
    }
}
