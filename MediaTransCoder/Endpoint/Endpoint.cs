namespace MediaTransCoder.Backend
{
    public class Endpoint : IDisposable {
        #region Fields
        public bool IsDebug {
            get {
                return context.IsDebug ?? false;
            }
            set {
                if (Context.IsSet) {
                    context.IsDebug = value;
                }
            }
        }
        public List<FileOption> Files { get; private set; } = new List<FileOption>();
        private int TotalSteps { get; set; }
        private EndpointOptions? Options { get; set; }
        private Context context;
        #endregion
        #region Constructor
        public Endpoint(BackendConfig config, IDisplay gui, bool? debug = null) {
            if (config == null) {
                throw new ArgumentNullException("Provided config was null!");
            }
            if (gui == null) {
                throw new ArgumentNullException("Provided gui was null!");
            }
            if(!Context.IsSet) {
                Context.Init(config, gui, debug);
            }
            context = Context.Get();
            context.Display = gui;
            Options = null;
        }
        #endregion

        #region Methods
        public void ConvertVideo(EndpointOptions options) {
            Options = options;
            Options.ValidateVideo();
            Files = new List<FileOption>();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            switch (Options.InputOption) {
                case InputOptions.FILE:
                    Files.Add(new FileOption(options));
                    break;
                case InputOptions.DIRECTORY:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetVideoExtensions(true));
                    break;
                case InputOptions.RECURSIVE:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetVideoExtensions(true), true);
                    break;
            }
            foreach(var file in Files) {
                var arg = FfmpegArgs.Get(Options, file);
                arg.GenerateOutputFileName();
                file.Output = arg.Files.Output;
                args.Add(arg);
            }
            DisplayFileList(Files);
            foreach (var arg in args) {
                using(var converter = new VideoConverter(arg, UpdateProgress, UpdateMetadata)) {
                    context.Display.Send("Converting file: \n\t" + arg.Files.ToString());
                    converter.Convert();
                }
            }
        }

        public void ConvertAudio(EndpointOptions options) {
            Options = options;
            Options.ValidateAudio();
            Files = new List<FileOption>();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            switch (Options.InputOption) {
                case InputOptions.FILE:
                    Files.Add(new FileOption(options));
                    break;
                case InputOptions.DIRECTORY:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetAudioExtensions(true));
                    break;
                case InputOptions.RECURSIVE:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetAudioExtensions(true), true);
                    break;
            }
            foreach (var file in Files) {
                var arg = FfmpegArgs.Get(Options, file);
                arg.GenerateOutputFileName();
                file.Output = arg.Files.Output;
                args.Add(arg);
            }
            DisplayFileList(Files);
            foreach (var arg in args) {
                using (var converter = new AudioConverter(arg, UpdateProgress, UpdateMetadata)) {
                    context.Display.Send("Converting file: \n\t" + arg.Files.ToString());
                    converter.Convert();
                }
            }
        }

        public void ConvertImage(EndpointOptions options) {
            Options = options;
            Options.ValidateImage();
            Files = new List<FileOption>();
            List<FfmpegArgs> args = new List<FfmpegArgs>();
            switch (Options.InputOption) {
                case InputOptions.FILE:
                    Files.Add(new FileOption(options));
                    break;
                case InputOptions.DIRECTORY:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetImageExtensions(true));
                    break;
                case InputOptions.RECURSIVE:
                    Files = FileOption.GetFileOptionsFromDirectory(Options.Input, Options.Output, FileExtensions.GetImageExtensions(true), true);
                    break;
            }
            foreach (var file in Files) {
                var arg = FfmpegArgs.Get(Options, file);
                arg.GenerateOutputFileName();
                file.Output = arg.Files.Output;
                args.Add(arg);
            }
            DisplayFileList(Files);
            foreach (var arg in args) {
                using (var converter = new ImageConverter(arg, UpdateProgress, UpdateMetadata)) {
                    context.Display.Send("Converting file: \n\t" + arg.Files.ToString());
                    converter.Convert();
                }
            }
        }

        public void Dispose() {

        }

        private void UpdateMetadata(FfmpegMetadata metadata) {
            if(Options?.Image == null) {
                if (Options?.AudioOnly ?? false) {
                    TotalSteps += metadata.Duration.TotalMiliseconds;
                } else {
                    TotalSteps += metadata.TotalNumberOfFrames;
                }
            } else {
                TotalSteps += metadata.TotalNumberOfFrames;
            }
        }

        private void UpdateProgress(int lastStep) {
            double progress = 0;
            progress = Math.Round((((double)lastStep / TotalSteps) * 100), 1);
            context.Display.UpdateProgress(progress);
        }

        private void DisplayFileList(List<FileOption>? files) {
            if(files != null && files.Count > 1) {
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