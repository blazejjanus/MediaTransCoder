namespace MediaTransCoder.Backend
{
    public class Endpoint : IDisposable {
        #region Fields
        private Context context;
        private int TotalSteps { get; set; }
        private List<AbstractConverter> Converters { get; set; }
        private EndpointOptions? Options { get; set; }
        #endregion
        #region Constructor
        public Endpoint(BackendConfig config, IDisplay gui, bool? debug = null) {
            if (config == null) {
                throw new ArgumentNullException("Provided config was null!");
            }
            if (gui == null) {
                throw new ArgumentNullException("Provided gui was null!");
            }
            Context.Init(config, gui, debug);
            context = Context.Get();
            context.Display = gui;
            Converters = new List<AbstractConverter>();
            Options = null;
        }
        #endregion

        #region Methods
        public void ConvertVideo(EndpointOptions options) {
            Options = options;
            Options.ValidateVideo();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            List<FileOption> files = new List<FileOption>();
            switch (Options.InputOption) {
                case InputOptions.FILE:
                    args.Add(FfmpegArgs.Get(Options));
                    break;
                case InputOptions.DIRECTORY:
                    files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetVideoExtensions(true));
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(Options, file.Input, file.Output));
                    }
                    break;
                case InputOptions.RECURSIVE:
                    files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetVideoExtensions(true), true);
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(Options, file.Input, file.Output));
                    }
                    break;
            }
            foreach(var arg in args) {
                arg.GenerateOutputFileName();
                var converter = new VideoConverter(arg, UpdateProgress, UpdateMetadata);
                Converters.Add(converter);
            }
            DisplayFileList(files);
            foreach(var converter in Converters) {
                context.Display.Send("Converting file: ", MessageType.SUCCESS);
                context.Display.Send("\t" + converter.InputFile, MessageType.SUCCESS);
                Logging.Debug("Output file name:\n\t" + converter.OutputFile);
                converter.Convert();
                converter.Dispose();
            }
        }

        public void ConvertAudio(EndpointOptions options) {
            Options = options;
            Options.ValidateVideo();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            List<FileOption> files = new List<FileOption>();
            switch (Options.InputOption) {
                case InputOptions.FILE:
                    args.Add(FfmpegArgs.Get(Options));
                    break;
                case InputOptions.DIRECTORY:
                    files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetAudioExtensions(true));
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(Options, file.Input, file.Output));
                    }
                    break;
                case InputOptions.RECURSIVE:
                    files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetAudioExtensions(true), true);
                    foreach (var file in files) {
                        args.Add(FfmpegArgs.Get(Options, file.Input, file.Output));
                    }
                    break;
            }
            foreach (var arg in args) {
                arg.GenerateOutputFileName();
                var converter = new AudioConverter(arg, UpdateProgress, UpdateMetadata);
                Converters.Add(converter);
            }
            DisplayFileList(files);
            foreach (var converter in Converters) {
                context.Display.Send("Converting file: ", MessageType.SUCCESS);
                context.Display.Send("\t" + converter.InputFile, MessageType.SUCCESS);
                Logging.Debug("Output file name:\n\t" + converter.OutputFile);
                converter.Convert();
                converter.Dispose();
            }
        }

        public void ConvertImage(EndpointOptions options) {
            Options = options;
            Options.ValidateImage();
        }

        public void Dispose() {
            foreach(var converter in Converters) {
                converter.Dispose();
            }
        }

        private void UpdateMetadata(FfmpegMetadata metadata) {
            if (Options?.AudioOnly ?? false) {
                TotalSteps += metadata.Duration.TotalMiliseconds;
            } else {
                TotalSteps += metadata.TotalNumberOfFrames;
            }
        }

        private void UpdateProgress(int lastStep) {
            double progress = 0;
            progress = Math.Round((((double)lastStep / TotalSteps) * 100), 1);
            Logging.Debug(progress.ToString());
            context.Display.UpdateProgress(progress);
        }

        private void DisplayFileList(List<FileOption>? files) {
            if(files != null) {
                foreach (var file in files) {
                    context.Display.Send(file.ToString());
                }
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
    }
}