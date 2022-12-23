using MediaTransCoder.Backend.Compatibility;

namespace MediaTransCoder.Backend {
    public class Endpoint {
        #region Fields
        private Context context;
        protected static Endpoint? instance;
        #endregion
        #region Constructor
        public Endpoint(BackendConfig config, IDisplay gui) {
            if (config == null) {
                throw new ArgumentNullException("Provided config was null!");
            }
            if (gui == null) {
                throw new ArgumentNullException("Provided gui was null!");
            }
            Context.Init(config, gui);
            context = Context.Get();
            context.Display = gui;
            instance = this;
        }

        #region Methods
        public void ConvertVideo(FfmpegArgs args) {
            args.ValidateVideo();
            using(var caller = new FfmpegCaller(args)) {
                caller.RunAsync();
            }
        }

        public void ConvertAudio(FfmpegArgs args) {

        }

        public void ConvertImage(FfmpegArgs args) {

        }
        #region Test
        public List<CompatibilityChart> TestAudio(string input, string? output = null) {
            return CompatibilityTest.Audio(input, output);
        }

        public List<CompatibilityChart> TestVideo(string input, string? output = null) {
            return CompatibilityTest.Video(input, output);
        }

        public List<CompatibilityChart> TestAudioVideo(string input, string? output = null) {
            return CompatibilityTest.AudioVideo(input, output);
        }
        #endregion
        #endregion
        #endregion
    }
}