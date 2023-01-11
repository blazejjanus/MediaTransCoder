namespace MediaTransCoder.Backend {
    public class Endpoint : IDisposable {
        #region Fields
        /// <summary>
        /// Determines if Backend is launched in debug mode (additional info will be provided)
        /// </summary>
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
        /// <summary>
        /// List of files being processed
        /// </summary>
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

        /// <summary>
        /// Video conversion endpoint
        /// </summary>
        /// <param name="options">Video conversion options, Format, Video and Audio (if not Video.RemoveAudio is false) must be provided</param>
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

        /// <summary>
        /// Audio conversion endpoint
        /// </summary>
        /// <param name="options">Audio conversion options, Format, and Audio must be provided</param>
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

        /// <summary>
        /// Image conversion endpoint
        /// </summary>
        /// <param name="options">Image conversion options, Image must be provided, either Format or Video must be null</param>
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

        public static BackendConfig FirstTimeSetup() {
            var config = new BackendConfig();
            if (DependencySetup.CheckEnvironmentalPaths()) {
                config.FfmpegPath = "ffmpeg";
            } else {
                config.FfmpegPath = null;
            }
            config.Hardware = HardwareDetection.GetConfig();
            return config;
        }

        public bool CheckFfmpegPath() {
            return DependencySetup.CheckFfmpegPath(context.Config?.FfmpegPath ?? "ffmpeg");
        }

        public bool CheckFfmpegPath(string path) {
            return DependencySetup.CheckFfmpegPath(path);
        }

        public Preset GetPreset(PresetType type, PresetTarget target, PresetQuality quality) {
            return PresetsService.Get(type, target, quality);
        }

        public Preset GetAudioPreset(PresetTarget target, PresetQuality quality) {
            return PresetsService.GetAudio(target, quality);
        }

        public Preset GetVideoPreset(PresetTarget target, PresetQuality quality) {
            return PresetsService.GetVideo(target, quality);
        }

        public Preset GetImagePreset(PresetTarget target, PresetQuality quality) {
            return PresetsService.GetImage(target, quality);
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
        #endregion
    }
}