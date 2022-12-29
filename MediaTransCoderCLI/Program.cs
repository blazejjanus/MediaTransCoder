using MediaTransCoder.Backend;
using System.IO;

namespace MediaTransCoder.CLI {
    internal class Program {
        private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static ProgressBar Progress = new ProgressBar();
        private static CLIDisplay GUI = CLIDisplay.GetInstance();
        private static string input = @"E:\TEMP\mtc\input\audio\";
        private static string output = @"E:\TEMP\mtc\output\audio\";
        static void Main(string[] args) {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            Config = CLIConfig.ReadConfig();
            if(Config== null) {
                throw new Exception("Obtained config was null!");
            }
            GUI.Progress = Progress;
            Backend = new Endpoint(Config.Backend, GUI);
            ConvertAudio();
        }

        #region Tests
        private static void ConvertVideo() {
            var options = new EndpointOptions() {
                Input = input,
                Output = output,
                InputOption = InputOptions.RECURSIVE,
                Format = ContainerFormat.matroska,
                Video = new VideoOptions() {
                    Codec = VideoCodecs.hevc,
                    Resolution = Resolutions.r1080p,
                    FPS = 60,
                    BitRate = 35000
                },
                Audio = new AudioOptions() {
                    Codec = AudioCodecs.mp3,
                    BitRate = 128,
                    AudioChannels = 1,
                    SamplingRate = 44100
                }
            };
            Backend?.ConvertVideo(options);
        }

        private static void ConvertAudio() {
            var options = new EndpointOptions() {
                Input = input,
                Output = output,
                InputOption = InputOptions.RECURSIVE,
                Format = ContainerFormat.ogg,
                Audio = new AudioOptions() {
                    Codec = AudioCodecs.libvorbis,
                    BitRate = 192,
                    AudioChannels = 2,
                    SamplingRate = 48000
                }
            };
            Backend?.ConvertAudio(options);
        }

        private static void ConvertImage() {

        }

        private static void GetExtensions(bool searchCriteria = false) {
            var audio = FileExtensions.GetAudioExtensions(searchCriteria);
            Console.WriteLine("##############################");
            Console.WriteLine("Audio Extensions:");
            foreach(var extension in audio) {
                Console.WriteLine("\t" + extension);
            }
            Console.WriteLine("##############################");
            var video = FileExtensions.GetVideoExtensions(searchCriteria);
            Console.WriteLine("Video Extensions:");
            foreach (var extension in video) {
                Console.WriteLine("\t" + extension);
            }
            Console.WriteLine("##############################");
        }

        private void Test() {
            var charts = Backend?.TestAudioVideo(input);
            File.Create(new FileInfo(input).Directory + "\\result.txt");
            if(charts != null) {
                foreach (var chart in charts) {
                    Console.WriteLine("");
                    Console.WriteLine(chart.ToString());
                }
            }
        }
        #endregion

        private static void Setup() {
            var cfg = CLIConfig.Defaults();
            var env = EnvironmentalSettings.Get();
            cfg.Backend.Environment = EnvironmentType.Development;
            cfg.Backend.TempDirPath = env.RootPath + ".temp//";
            cfg.Backend.Logging.LogFilePath = env.LogPath;
            cfg.Backend.Logging.LoggingLevel = LoggingLevel.INFO;
            cfg.SaveConfig(env.ConfigPath + "config.json");
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs e) {
            e.Cancel = true;
            Backend?.Dispose();
            Environment.ExitCode = 0;
            Environment.Exit(0);
        }
    }
}