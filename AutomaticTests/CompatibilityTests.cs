using MediaTransCoder.Backend;
using MediaTransCoder.Shared;

namespace MediaTransCoder.Tests {
    public static class CompatibilityTests {
        private static Endpoint? Backend;
        private static BackendConfig? Config;
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static TestDisplay Display = TestDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(Display);

        static CompatibilityTests() {
            Config = new BackendConfig();
            Config.Hardware.GPU = GPUType.NONE;
            Config.Hardware.CPUCores = 16;
            Config.Environment = EnvironmentType.Test;
            Backend = new Endpoint(Config, Display);
            Display.ShowProgress = false;
        }

        public static void TestCompatibilityInfo(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            Display.LogFile = Pathes.LogDirectory + "\\compatibility.log";
            Display.ShowProgress = verbose;
            TryFfmpeg? caller = null;
            Validator.RemoveEmptyDirs(testEnv.Video.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            EndpointOptions options = new EndpointOptions();
            //TestCompatiblityCharts video
            Display.Log("Video tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleVideoOptions();
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                Display.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                options.Format = format;
                foreach (var vcodec in videoCodecs) {
                    if(options.Video != null && options.Audio != null) {
                        options.Video.Codec = vcodec;
                        options.Output = testEnv.Video.Output + format + "\\" + vcodec;
                        foreach (var acodec in audioCodecs) {
                            options.Audio.Codec = acodec;
                            options.OutputFileName = vcodec + "_" + acodec;
                            Display.Log(vcodec + "_" + acodec + ": ");
                            caller?.Audio(options);
                        }
                    }
                }
            }
            //TestCompatiblityCharts audio
            Display.Log("Audio tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleAudioOptions();
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                Display.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                options.Format = format;
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                foreach (var acodec in audioCodecs) {
                    if (options.Audio != null) {
                        options.Output = testEnv.Video.Output + format + "\\" + acodec;
                        options.Audio.Codec = acodec;
                        caller?.Audio(options);
                    }
                }
            }
            //Cleanup
            Validator.RemoveEmptyDirs(testEnv.Video.Output);
        }

        public static void TestCodecResolutionCompatibility() {
            Display.LogFile = Pathes.LogDirectory + "\\resolutions.log";
            TryFfmpeg? caller = null;
            if (Backend != null) {
                Backend.IsDebug = false;
                caller = new TryFfmpeg(Backend);
            }
            var options = EndpointOptions.GetSampleVideoOptions();
            if (options.Video == null) {
                throw new Exception("Cannot get valid video options!");
            }
            string originalOutput = options.Output;
            foreach (VideoCodecs codec in Enum.GetValues(typeof(VideoCodecs))) {
                Display.Log("\n" + codec + ":", MessageType.SUCCESS);
                options.Video.Codec = codec;
                options.Output = originalOutput + "\\" + codec;
                foreach (Resolutions resolution in Enum.GetValues(typeof(Resolutions))) {
                    Display.Log("\t" + resolution + ":", MessageType.SUCCESS);
                    options.Video.Resolution = resolution;
                    options.OutputFileName = resolution.ToString();
                    caller?.Video(options);
                }
            }
        }

        public static void TestCompatiblityCharts() {
            var testEnv = TestingEnvironment.Get();
            string input = testEnv.CurrentRootPath + "\\charts";
            if(!Directory.Exists(input)) {
                Directory.CreateDirectory(input);
            }
            Display.LogFile = Pathes.LogDirectory + "\\charts.log";
            var charts = Backend?.TestAudioVideo(input);
            if (charts != null) {
                Display.Log("Compatibility charts:\n");
                foreach (var chart in charts) {
                    Display.Log(chart.ToString());
                }
            }
        }
    }
}
