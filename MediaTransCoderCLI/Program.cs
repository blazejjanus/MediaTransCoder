using MediaTransCoder.Backend;
using MediaTransCoder.Shared;

namespace MediaTransCoder.CLI {
    internal class Program {
        private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static CLIDisplay GUI = CLIDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(GUI);
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static string input = @"E:\TEMP\mtc\input\video\";
        private static string output = @"E:\TEMP\mtc\output\video\";

        static void Main(string[] args) {
            Setup();
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            Config = CLIConfig.ReadConfig();
            if(Config == null) {
                throw new Exception("Obtained config was null!");
            }
            GUI.Progress = new ProgressBar();
            Backend = new Endpoint(Config.Backend, GUI);
            //TestExtensionsGeneration();
            //GetCompatibilityLists(true);
            //GetExtensions();
            //ConvertVideo();
            //ConvertAudio();
            TestCompatibilityInfo(true);
            //TestCodecResolutionCompatibility();
        }

        #region Tests
        private static void ConvertVideo() {
            var options = EndpointOptions.GetSampleVideoOptions();
            Backend?.ConvertVideo(options);
        }

        private static void ConvertAudio() {
            var options = EndpointOptions.GetSampleAudioOptions();
            Backend?.ConvertAudio(options);
        }

        private static void ConvertImage() {

        }

        private static void TestCompatibilityInfo(bool useOneAcodecForVideo = true) {
            var testEnv = TestingEnvironment.Get();
            GUI.LogFile = Pathes.LogDirectory + "\\compatibility.log";
            if (File.Exists(GUI.LogFile)) {
                if(File.Exists(GUI.LogFile + ".old")) {
                    File.Delete(GUI.LogFile + ".old");
                }
                File.Move(GUI.LogFile, GUI.LogFile + ".old");
            }
            TryFfmpeg? caller = null;
            Validator.RemoveEmptyDirs(testEnv.Video.Output);
            if (Backend != null) {
                Backend.IsDebug = false;
                caller = new TryFfmpeg(Backend);
            }
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            //Test video
            GUI.Log("Video tests:\n\n", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                GUI.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                foreach(var vcodec in videoCodecs) {
                    var options = new EndpointOptions() {
                        Input = testEnv.Video.Input,
                        Output = testEnv.Video.Output + format + "\\" + vcodec,
                        InputOption = InputOptions.FILE,
                        Format = format,
                        Video = new VideoOptions() {
                            Codec = vcodec,
                            Resolution = Resolutions.r1080p,
                            FPS = 60,
                            BitRate = 35000
                        }
                    };
                    if (useOneAcodecForVideo) {
                        GUI.Log(vcodec + ": ");
                        options.Audio = new AudioOptions() {
                            Codec = AudioCodecs.mp3,
                            BitRate = 128,
                            AudioChannels = 2,
                            SamplingRate = 48000
                        };
                        caller?.Audio(options);
                    } else {
                        foreach (var acodec in audioCodecs) {
                            options.Output += "_" + acodec;
                            options.Audio = new AudioOptions() {
                                Codec = acodec,
                                BitRate = 128,
                                AudioChannels = 2,
                                SamplingRate = 48000
                            };
                            GUI.Log(vcodec + "_" + acodec + ": ");
                            caller?.Audio(options);
                        }
                    }
                }
            }
            //Test audio
            GUI.Log("Audio tests:\n\n", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                GUI.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                foreach (var acodec in audioCodecs) {
                    var options = new EndpointOptions() {
                        Input = testEnv.Audio.Input,
                        Output = testEnv.Video.Output + format + "\\",
                        InputOption = InputOptions.FILE,
                        Format = format,
                        Audio = new AudioOptions() {
                            Codec = acodec,
                            BitRate = 128,
                            AudioChannels = 2,
                            SamplingRate = 48000
                        }
                    };
                    caller?.Audio(options);
                }
            }
            //Cleanup
            Validator.RemoveEmptyDirs(testEnv.Video.Output);
        }

        private static void TestCodecResolutionCompatibility() {
            GUI.LogFile = Pathes.LogDirectory + "\\resolutions.log";
            if (File.Exists(GUI.LogFile)) {
                if (File.Exists(GUI.LogFile + ".old")) {
                    File.Delete(GUI.LogFile + ".old");
                }
                File.Move(GUI.LogFile, GUI.LogFile + ".old");
            }
            TryFfmpeg? caller = null;
            if (Backend != null) {
                Backend.IsDebug = false;
                caller = new TryFfmpeg(Backend);
            }
            var options = EndpointOptions.GetSampleVideoOptions();
            if(options.Video == null) {
                throw new Exception("Cannot get valid video options!");
            }
            string originalOutput = options.Output;
            foreach (VideoCodecs codec in Enum.GetValues(typeof(VideoCodecs))) {
                GUI.Log("\n" + codec + ":", MessageType.SUCCESS);
                options.Video.Codec = codec;
                options.Output = originalOutput + "\\" + codec;
                foreach (Resolutions resolution in Enum.GetValues(typeof(Resolutions))) {
                    GUI.Log("\t" + resolution + ":", MessageType.SUCCESS);
                    options.Video.Resolution = resolution;
                    caller?.Video(options, resolution.ToString());
                }
            }
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
            cfg.SaveConfig(env.ConfigPath + "config.json");
            TestingEnvironment.RootPath = @"E:\TEMP\mtc";
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs e) {
            e.Cancel = true;
            Backend?.Dispose();
            Environment.ExitCode = 0;
            Environment.Exit(0);
        }
    }
}