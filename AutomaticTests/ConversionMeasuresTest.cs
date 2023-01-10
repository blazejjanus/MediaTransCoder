using MediaTransCoder.Backend;
using MediaTransCoder.Shared;

namespace MediaTransCoder.Tests {
    public static class ConversionMeasuresTest {
        private static Endpoint? Backend;
        private static BackendConfig? Config;
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static TestDisplay Display = TestDisplay.GetInstance();
        private static ConversionValidator Validator = new ConversionValidator(Display);
        private static string? LogPath = null;

        static ConversionMeasuresTest() {
            Config = new BackendConfig();
            Config.Hardware.GPU = GPUType.NONE;
            Config.Hardware.CPUCores = 16;
            Config.Environment = EnvironmentType.Test;
            Backend = new Endpoint(Config, Display);
            Display.ShowProgress = false;
        }

        public static void Measure(bool verbose = false) {
            LogPath = Pathes.LogDirectory + "\\meassres";
            if(!Directory.Exists(LogPath)) { 
                Directory.CreateDirectory(LogPath);
            }
            LogPath += "\\all.log";
            Display.LogFile = LogPath;
            MeasureAudio(verbose);
            Display.Log("\n\n");
            MeasureVideo(verbose);
            Display.Log("\n\n");
            MeasureImage(verbose);
        }

        public static void MeasureAudio(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            if(LogPath == null) {
                LogPath = Pathes.LogDirectory + "\\audio_meassure.log";
            }
            Display.LogFile = LogPath;
            TryFfmpeg? caller = null;
            Validator.RemoveEmptyDirs(testEnv.Audio.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            EndpointOptions options = new EndpointOptions();
            Display.Log("Audio meassuring tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleAudioOptions();
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                Display.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                options.Format = format;
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
                options.Output = testEnv.Audio.Output + format;
                foreach (var acodec in audioCodecs) {
                    if (options.Audio != null) {
                        options.OutputFileName = acodec.ToString();
                        options.Audio.Codec = acodec;
                        caller?.Audio(options);
                    }
                }
            }
        }

        public static void MeasureVideo(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            if (LogPath == null) {
                LogPath = Pathes.LogDirectory + "\\video_meassure.log";
            }
            Display.LogFile = LogPath;
            TryFfmpeg? caller = null;
            Validator.RemoveEmptyDirs(testEnv.Video.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            List<ContainerFormats> videoCodecs = new List<ContainerFormats>();
            EndpointOptions options = new EndpointOptions();
            Display.Log("Video meassuring tests:\n\n", MessageType.SUCCESS);
            options = EndpointOptions.GetSampleVideoOptions();
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                Display.Log("Testing " + format + ":\n", MessageType.SUCCESS);
                videoCodecs = Compatibility.GetCompatibleVideoCodecs(format);
                options.Format = format;
                foreach (var vcodec in videoCodecs) {
                    if (options.Video != null && options.Audio != null) {
                        options.Video.Codec = vcodec;
                        options.Output = testEnv.Video.Output + format;
                        options.Audio.Codec = Compatibility.GetDefaultAudioCodec(format);
                        options.Audio.SamplingRate = SamplingFrequency.ar48k;
                        options.OutputFileName = vcodec.ToString();
                        Display.Log(vcodec + ": ");
                        caller?.Audio(options);
                    }
                }
            }
        }

        public static void MeasureImage(bool verbose = false) {
            var testEnv = TestingEnvironment.Get();
            if (LogPath == null) {
                LogPath = Pathes.LogDirectory + "\\image_meassure.log";
            }
            Display.LogFile = LogPath;
            TryFfmpeg? caller = null;
            Validator.RemoveEmptyDirs(testEnv.Image.Output);
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            var options = EndpointOptions.GetSampleImageOptions();
            Display.Log("Image meassuring tests:\n\n", MessageType.SUCCESS);
            if (options.Image == null) {
                throw new NullReferenceException();
            }
            foreach (ImageFormat format in Enum.GetValues(typeof(ImageFormat))) {
                Display.Log(format + ": ");
                options.Image.Format = format;
                options.OutputFileName = format.ToString();
                caller?.Image(options);
            }
        }
    }
}
