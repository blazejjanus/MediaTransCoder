namespace MediaTransCoder.Backend {
    public class Endpoint : IDisposable {
        #region Fields
        private Context context;
        private List<FfmpegCaller> callers;
        private int TotalFrames { get; set; }
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
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            List<FileOption> files = new List<FileOption>();
            switch (options.InputOption) {
                case InputOptions.FILE:
                    args.Add(FfmpegArgs.Get(options));
                    break;
                case InputOptions.DIRECTORY:
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output);
                    foreach (var file in files) {

                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
                case InputOptions.RECURSIVE:
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output, "*.*", true);
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
            }
            foreach(var arg in args) {
                arg.GenerateOutputFileName();
                var caller = new FfmpegCaller(arg, UpdateProgress, UpdateMetadata);
                callers.Add(caller);
            }
            foreach(var caller in callers) {
                caller.Run();
                caller.Dispose();
            }
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

        private void UpdateMetadata(FfmpegVideoDetection metadata) {
            TotalFrames += metadata.TotalNumberOfFrames;
        }

        private void UpdateProgress(int lastframe) {
            double progress = 0;
            progress = Math.Round((((double)lastframe / TotalFrames) * 100), 1);
            if(context.Config.Environment == EnvironmentType.Development) {
                context.Display.Send(progress.ToString());
            }
            context.Display.UpdateProgress(progress);
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