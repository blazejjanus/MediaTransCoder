using MediaTransCoder.Backend;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace MediaTransCoder.CLI {
    internal class Program {
        //private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static IDisplay GUI = CLIDisplay.GetInstance();
        static void Main(string[] args) {
            string path = @"E:\TEMP\mtc\input\sample2.mp4";
            Config = CLIConfig.ReadConfig();
            if(Config== null) {
                throw new Exception("Obtained config was null!");
            }
            var backend = new Endpoint(Config.Backend, GUI);
            var ffmpegArgs = new FfmpegArgs() {
                InputPath = path,
                OutputPath = Path.GetDirectoryName(path) + "\\output\\result.mkv",
                Format = ContainerFormat.matroska,
                VideoOptions = new VideoOptions() {
                    Codec = VideoCodecs.hevc,
                    Resolution = Resolutions.r1440p,
                    FPS = 60,
                    BitRate = 35000
                },
                AudioOptions = new AudioOptions() {
                    Codec = AudioCodecs.mp3,
                    BitRate = 128
                }
            };
            backend.ConvertVideo(ffmpegArgs);
            //Read(path);
            /*
            var charts = backend.TestAudioVideo(path);
            File.Create(new FileInfo(path).Directory + "\\result.txt");
            foreach(var chart in charts) {
                Console.WriteLine("");
                Console.WriteLine(chart.ToString());
            }
            */
        }

        public static void Read(string filePath) {
            string processOutput = string.Empty;
            // Run the ffmpeg command with the vstats file output redirected to stdout
            Process process = new Process {
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

        private static void ParseFfmpegOutput(string output) {
            string duration = string.Empty;
            string fps = string.Empty;

            if (output.Contains("Duration:")) {
                output = output.Substring(output.IndexOf("Duration:"));
                int Pos1 = output.IndexOf("Duration:") + "Duration:".Length;
                int Pos2 = output.IndexOf(",");
                duration = output.Substring(Pos1, Pos2 - Pos1).Trim();
            }
            if (output.Contains("Video:")) {
                int Pos1 = output.IndexOf("Video:") + "Video:".Length;
                int Pos2 = output.IndexOf("fps") + "fps".Length;
                string temp = output.Substring(Pos1, Pos2 - Pos1).Trim();
                string[] lines = temp.Split(',');
                foreach(var line in lines) {
                    if (line.Contains("fps")) {
                        fps = line.Split("fps").First().Trim();
                    }
                }
            }

            Console.WriteLine("\n\n" + duration);
            Console.WriteLine(fps);
        }

        private static void Setup() {
            var cfg = CLIConfig.Defaults();
            var env = EnvironmentalSettings.Get();
            cfg.Backend.Environment = EnvironmentType.Development;
            cfg.Backend.TempDirPath = env.RootPath + ".temp//";
            cfg.Backend.Logging.LogFilePath = env.LogPath;
            cfg.Backend.Logging.LoggingLevel = LoggingLevel.INFO;
            cfg.SaveConfig(env.ConfigPath + "config.json");
        }
    }
}