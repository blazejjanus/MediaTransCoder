using MediaTransCoder.Backend;

namespace MediaTransCoder.CLI {
    internal class Program {
        private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static CLIDisplay GUI = CLIDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(GUI);
        private static string input = @"E:\TEMP\mtc\input\video\";
        private static string output = @"E:\TEMP\mtc\output\video\";
        private static string rootDir = string.Empty;
        private static string logDir = string.Empty;
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
            TestCompatibilityInfo();
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

        private static void GetCompatibilityLists(bool file = false) {
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            string content = string.Empty;
            foreach(ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                content += EnumHelper.GetName(format) + ":\n";
                content += "\tAudio:\n";
                foreach (var codec in audioCodecs) {
                    content += "\t\t" + EnumHelper.GetName(codec) + "\n";
                }
                content += "\tVideo:\n";
                foreach (var codec in videoCodecs) {
                    content += "\t\t" + EnumHelper.GetName(codec) + "\n";
                }
                content += "##############################\n\n";
            }
            Console.Write(content);
            if(file) {
                File.WriteAllText(rootDir + "\\compatibility_info.txt", content);
            }
        }

        private static void TestCompatibilityInfo() {
            string outputPath = @"E:\TEMP\mtc\output\compatibility\";
            string audioInput = @"E:\TEMP\mtc\input\compatibility\sample.mp3";
            string videoInput = @"E:\TEMP\mtc\input\compatibility\sample.mp4";
            GUI.LogFile = logDir + "\\compatibility.log";
            if (File.Exists(GUI.LogFile)) {
                if(File.Exists(GUI.LogFile + ".old")) {
                    File.Delete(GUI.LogFile + ".old");
                }
                File.Move(GUI.LogFile, GUI.LogFile + ".old");
            }
            if(Backend != null) {
                Backend.IsDebug = false;
            }
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            Resolutions defaultResolution = Resolutions.r1080p;
            Resolutions dvResolution = Resolutions.r720p;
            Resolutions resolution = defaultResolution;
            //Test video
            GUI.Log("Video tests:\n\n", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                GUI.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                foreach(var vcodec in videoCodecs) {
                    //Workaround for DV
                    if(vcodec == VideoCodecs.dvvideo) {
                        resolution = dvResolution;
                    } else {
                        resolution = defaultResolution;
                    }
                    foreach (var acodec in audioCodecs) {
                        var options = new EndpointOptions() {
                            Input = videoInput,
                            Output = outputPath + format + "\\" + vcodec + "_" + acodec,
                            InputOption = InputOptions.FILE,
                            Format = ContainerFormat.matroska,
                            Video = new VideoOptions() {
                                Codec = vcodec,
                                Resolution = resolution,
                                FPS = 60,
                                BitRate = 35000
                            },
                            Audio = new AudioOptions() {
                                Codec = acodec,
                                BitRate = 128,
                                AudioChannels = 2,
                                SamplingRate = 48000
                            }
                        };
                        GUI.Log(vcodec + "_" + acodec + ": ");
                        try {
                            Backend?.ConvertVideo(options);
                            foreach(var file in Backend?.Files ?? new List<FileOption>()) {
                                Validator.Validate(file.Output);
                            }
                            GUI.Log("OK\n", MessageType.SUCCESS);
                        } catch(Exception exc) {
                            string errorMessage = "Error while processing: " + format + " " + vcodec + " " + acodec;
                            errorMessage += "\nException:\n" + exc.Message;
                            if (exc.InnerException != null) {
                                errorMessage += "\nInner:\n" + exc.InnerException.Message;
                            }
                            errorMessage += "\n\n";
                            GUI.Log(errorMessage, MessageType.ERROR);
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
                        Input = audioInput,
                        Output = outputPath + format + "\\",
                        InputOption = InputOptions.FILE,
                        Format = format,
                        Audio = new AudioOptions() {
                            Codec = acodec,
                            BitRate = 128,
                            AudioChannels = 2,
                            SamplingRate = 48000
                        }
                    };
                    try {
                        Backend?.ConvertAudio(options);
                        foreach (var file in Backend?.Files ?? new List<FileOption>()) {
                            Validator.Validate(file.Output);
                        }
                        GUI.Log("OK\n", MessageType.SUCCESS);
                    } catch (Exception exc) {
                        string errorMessage = "Error while processing: " + format + " " + acodec;
                        errorMessage += "\nException:\n" + exc.Message;
                        if (exc.InnerException != null) {
                            errorMessage += "\nInner:\n" + exc.InnerException.Message;
                        }
                        errorMessage += "\n\n";
                        GUI.Log(errorMessage, MessageType.ERROR);
                    }
                }
            }
            //Cleanup
            Validator.RemoveEmptyDirs(outputPath);
        }

        private static void TestExtensionsGeneration() {
            GUI.LogFile = logDir + "\\extensions.log";
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            if (File.Exists(GUI.LogFile)) {
                if (File.Exists(GUI.LogFile + ".old")) {
                    File.Delete(GUI.LogFile + ".old");
                }
                File.Move(GUI.LogFile, GUI.LogFile + ".old");
            }
            //Test video
            GUI.Log("\nVideo:", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                GUI.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                foreach (var vcodec in videoCodecs) {
                    foreach (var acodec in audioCodecs) {
                        string exc = FfmpegArgs.GenerateOutputFileExtension(format, vcodec, acodec);
                        GUI.Log("\t\t" + format + "_" + vcodec + "_" + acodec + ": " + exc);
                    }
                }
            }
            //Test audio
            GUI.Log("\nAudio:", MessageType.SUCCESS);
            var formats = EnumHelper.GetAudioFormats();
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                GUI.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                foreach (var acodec in audioCodecs) {
                    string exc = FfmpegArgs.GenerateOutputFileExtension(format, null, acodec, true);
                    GUI.Log("\t\t" + format + "_" + acodec + ": " + exc);
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
            rootDir = Directory.GetCurrentDirectory();
            rootDir = rootDir.Split(".build").First();
            logDir = Path.Combine(rootDir, ".logs");
            if(!Directory.Exists(logDir)) { 
                Directory.CreateDirectory(logDir);
            }
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs e) {
            e.Cancel = true;
            Backend?.Dispose();
            Environment.ExitCode = 0;
            Environment.Exit(0);
        }
    }
}