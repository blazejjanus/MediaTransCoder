namespace MediaTransCoder.Backend
{
    public class Endpoint : IDisposable {
        #region Fields
        private Context context;
        private int TotalFrames { get; set; }
        private List<AbstractConverter> Converters { get; set; }
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
            Converters = new List<AbstractConverter>();
        }
        #endregion

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
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output, FileExtensions.GetVideoExtensions(true));
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
                case InputOptions.RECURSIVE:
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output, FileExtensions.GetVideoExtensions(true), true);
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
            }
            foreach(var arg in args) {
                arg.GenerateOutputFileName();
                var converter = new VideoConverter(arg, UpdateProgress, UpdateMetadata);
                Converters.Add(converter);
            }
            foreach(var converter in Converters) {
                converter.Convert();
                converter.Dispose();
            }
        }

        public void ConvertAudio(EndpointOptions options) {
            options.ValidateAudio();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            List<FileOption> files = new List<FileOption>();
            switch (options.InputOption) {
                case InputOptions.FILE:
                    args.Add(FfmpegArgs.Get(options));
                    break;
                case InputOptions.DIRECTORY:
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output, FileExtensions.GetAudioExtensions(true));
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
                case InputOptions.RECURSIVE:
                    files = FileOption.GetFileOptionsFromDirectory(options.Input, options.Output, FileExtensions.GetAudioExtensions(true), true);
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(options, file.Input, file.Output));
                    }
                    break;
            }
            foreach (var arg in args) {
                arg.GenerateOutputFileName();
                var converter = new AudioConverter(arg, UpdateProgress, UpdateMetadata);
                Converters.Add(converter);
            }
            foreach (var converter in Converters) {
                converter.Convert();
                converter.Dispose();
            }
        }

        public void ConvertImage(EndpointOptions options) {

        }

        public void Dispose() {
            foreach(var converter in Converters) {
                converter.Dispose();
            }
        }

        private void UpdateMetadata(FfmpegMetadata metadata) {
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
    }
}