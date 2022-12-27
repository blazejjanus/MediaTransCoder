using MediaTransCoder.Backend.Compatibility;

namespace MediaTransCoder.Backend {
    public class Endpoint : IDisposable {
        #region Fields
        private Context context;
        private List<FfmpegCaller> callers;
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
            callers = new List<FfmpegCaller>();
            instance = this;
        }

        #region Methods
        public void ConvertVideo(EndpointOptions options) {
            options.ValidateVideo();
            callers.Add(new FfmpegCaller(options));
            callers.First().Run(); //TODO: Rethink for recursive mode
            callers.First().Dispose();
        }

        public void ConvertAudio(EndpointOptions options) {

        }

        public void ConvertImage(EndpointOptions options) {

        }

        public void Dispose() {
            foreach(var caller in callers) {
                caller.Dispose();
            }
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