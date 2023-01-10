using MediaTransCoder.Backend;
using MediaTransCoder.Shared;

namespace MediaTransCoder.Tests {
    public static class CompatibilityTests {
        private static Endpoint? Backend;
        private static BackendConfig? Config;
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static TestDisplay Display = TestDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(Display);
        private static string? LogPath = null;

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
            List<ContainerFormats> videoCodecs = new List<ContainerFormats>();
            EndpointOptions options = new EndpointOptions();
            //TestCompatiblityCharts video
            Display.Log("Video tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleVideoOptions();
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                Display.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
                videoCodecs = Compatibility.GetCompatibleVideoCodecs(format);
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
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
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
            foreach (ContainerFormats codec in Enum.GetValues(typeof(ContainerFormats))) {
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

        public static void TestAudioSamplingFrequency(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            if (LogPath == null) {
                LogPath = Pathes.LogDirectory + "\\audio_sampling_frequency.log";
            }
            Display.LogFile = LogPath;
            TryFfmpeg? caller = null;
            var result = new Dictionary<AudioCodecs, List<SamplingFrequency>>();
            Validator.RemoveEmptyDirs(testEnv.Audio.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            EndpointOptions options = new EndpointOptions();
            Display.Log("Audio meassuring tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleAudioOptions();
            options.Output = testEnv.Audio.Output;
            foreach (AudioCodecs codec in Enum.GetValues(typeof(AudioCodecs))) {
                var compat = new List<SamplingFrequency>();
                Display.Log("Testing " + codec + ":\n", MessageType.SUCCESS);
                options.Format = Compatibility.GetDefaultFormat(codec);
                options.Output = testEnv.Audio.Output + codec;
                foreach (SamplingFrequency sf in Enum.GetValues(typeof(SamplingFrequency))) {
                    if (options.Audio != null) {
                        options.OutputFileName = sf.ToString();
                        options.Audio.Codec = codec;
                        if (caller?.Audio(options) ?? false) {
                            compat.Add(sf);
                        }
                    }
                }
                result.Add(codec, compat);
            }
            //Dispaly summary
            foreach(var entry in result) {
                Display.Log(entry.Key + ":");
                foreach(var sf in entry.Value) {
                    Display.Log("\t" + sf);
                }
                Display.Log("");
            }
        }

        public static void TestAudioBitRate(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            if (LogPath == null) {
                LogPath = Pathes.LogDirectory + "\\audio_bitrate.log";
            }
            Display.LogFile = LogPath;
            TryFfmpeg? caller = null;
            var result = new Dictionary<AudioCodecs, List<AudioBitRate>>();
            Validator.RemoveEmptyDirs(testEnv.Audio.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            EndpointOptions options = new EndpointOptions();
            Display.Log("Audio meassuring tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleAudioOptions();
            options.Output = testEnv.Audio.Output;
            foreach (AudioCodecs codec in Enum.GetValues(typeof(AudioCodecs))) {
                var compat = new List<AudioBitRate>();
                Display.Log("Testing " + codec + ":\n", MessageType.SUCCESS);
                options.Format = Compatibility.GetDefaultFormat(codec);
                options.Output = testEnv.Audio.Output + codec;
                foreach (AudioBitRate abr in Enum.GetValues(typeof(AudioBitRate))) {
                    if (options.Audio != null) {
                        options.OutputFileName = abr.ToString();
                        options.Audio.Codec = codec;
                        if (caller?.Audio(options) ?? false) {
                            compat.Add(abr);
                        }
                    }
                }
                result.Add(codec, compat);
            }
            //Dispaly summary
            foreach (var entry in result) {
                Display.Log(entry.Key + ":");
                foreach (var abr in entry.Value) {
                    Display.Log("\t" + abr);
                }
                Display.Log("");
            }
        }
    }
}
