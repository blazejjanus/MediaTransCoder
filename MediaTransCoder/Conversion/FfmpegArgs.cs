using System.Text;

namespace MediaTransCoder.Backend {
    public class FfmpegArgs {
        public string FfmpegPath { get; set; }
        public FileOption Files { get; set; }
        public bool OverrideExistingFiles { get; set; }
        public bool SkipExistingFiles { get; set; }
        public bool AudioOnly { get; set; }
        public HardwareAcceleration Acceleration { get; set; }
        public ContainerFormat? Format { get; set; }
        public AudioOptions? Audio { get; set; }
        public VideoOptions? Video { get; set; }
        public ImageOptions? Image { get; set; }

        public FfmpegArgs() {
            FfmpegPath = Context.Get().Config.FfmpegPath ?? "ffmpeg";
            Files = new FileOption();
            OverrideExistingFiles = true;
            AudioOnly = false;
            SkipExistingFiles = true;
            Acceleration = HardwareAcceleration.NONE;
        }

        internal static FfmpegArgs Get(EndpointOptions options, string input, string output) {
            var result = new FfmpegArgs();
            result.Files.Input = input;
            result.Files.Output = output;
            result.AudioOnly = options.AudioOnly;
            result.OverrideExistingFiles = options.OverrideExistingFiles;
            result.SkipExistingFiles = options.SkipExistingFiles;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            result.Image = options.Image;
            return result;
        }

        internal static FfmpegArgs Get(EndpointOptions options, FileOption files) {
            var result = new FfmpegArgs();
            result.Files = files;
            result.AudioOnly = options.AudioOnly;
            result.OverrideExistingFiles = options.OverrideExistingFiles;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            result.Image = options.Image;
            return result;
        }

        internal static FfmpegArgs Get(EndpointOptions options) {
            if (options.InputOption != InputOptions.FILE) {
                throw new Exception("Cannot convert Endpoint Options to FfmpegArgs!");
            }
            var result = new FfmpegArgs();
            result.Files.Input = options.Input;
            result.Files.Output = options.Output;
            result.AudioOnly = options.AudioOnly;
            result.OverrideExistingFiles = options.OverrideExistingFiles;
            result.Format = options.Format;
            result.Acceleration = options.Acceleration;
            result.Audio = options.Audio;
            result.Video = options.Video;
            result.Image = options.Image;
            return result;
        }

        internal string GetArgs() {
            StringBuilder sb = new StringBuilder();
            sb.Append("-hide_banner"); //Hide unused banner info
            sb.Append(" -loglevel " + Logging.LoggingLevel); //Set logging level
            sb.Append(" -progress -");
            if (OverrideExistingFiles) {
                sb.Append(" -y"); //Override existing output files?
            }
            if (Acceleration == HardwareAcceleration.CPU) {
                sb.Append(" -threads " + Context.Get().Config.Hardware.CPUCores);
                //TODO: Mechanism of choosing codecs for GPU acceleration
            }
            sb.Append(" -i \"" + Files.Input + "\""); //Single file path
            if(Video != null) {
                if(Video.Codec != VideoCodecs.gifv) {
                    sb.Append(" -vcodec " + EnumHelper.GetName(Video.Codec));
                }
                sb.Append(" -r " + Video.FPS);
                sb.Append(" -vf \"scale=" + EnumHelper.GetResolution(Video.Resolution));
                if(Video.Codec == VideoCodecs.gifv) {
                    sb.Append(":flags=lanczos,split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse\"");
                } else {
                    sb.Append("\"");
                }
                sb.Append(" -b:v " + Video.BitRate + "k");
            }
            if(Audio != null) {
                sb.Append(" -acodec " + EnumHelper.GetName(Audio.Codec));
                sb.Append(" -b:a " + Audio.BitRate + "k");
                sb.Append(" -ar " + Audio.SamplingRate);
                sb.Append(" -ac " + Audio.AudioChannels);
            }
            if(Image != null) {
                if(Image.CompressionLevel != null) {
                    sb.Append(" -qscale:v " + Image.CompressionLevel);
                }
                sb.Append(" -pix_fmt " + EnumHelper.GetName(Image.PixelFormat));
                sb.Append(Image.GetVF());
                sb.Append(" -c " + EnumHelper.GetCommand(Image.Format));
            } else {
                if (Format != null) {
                    sb.Append(" -f " + EnumHelper.GetName(Format.Value));
                }
            }
            if(Video != null && Video.Codec == VideoCodecs.gifv) {
                sb.Append(" -loop 0");
            }
            sb.Append(" \"" + Files.Output + "\"");
            return sb.ToString();
        }

        public void GenerateOutputFileName() {
            if (Files.OutputFileName == null) {
                Files.OutputFileName = Path.GetFileNameWithoutExtension(Files.Input);
            }
            Files.OutputFileName += GenerateOutputFileExtension(Format, Image?.Format, Video?.Codec, Audio?.Codec, AudioOnly);
            if (Files.Output.EndsWith("..")) {
                Files.Output = Files.Output.Split("..").First();
            }
            Files.Output = Path.Combine(Files.Output, Files.OutputFileName);
        }

        public static string GenerateOutputFileExtension(ContainerFormat? containerFormat, ImageFormat? imageFormat, VideoCodecs? vcodec, AudioCodecs? acodec, bool audioOnly = false) {
            string name = string.Empty;
            if(imageFormat != null) {
                name = GetImageExtension(imageFormat.Value);
            } else {
                if(containerFormat == null) {
                    throw new ArgumentNullException(nameof(containerFormat));
                }
                name = GetAudioVideoExtension(containerFormat.Value, vcodec, acodec, audioOnly);
            }
            return name;
        }

        private static string GetAudioVideoExtension(ContainerFormat? format, VideoCodecs? vcodec, AudioCodecs? acodec, bool audioOnly = false) {
            string name = string.Empty;
            string? containerExtension = null;
            string? codecExtension = null;
            if (format != null) {
                containerExtension = EnumHelper.GetFileExtension(format.Value, audioOnly);
            }
            if (vcodec != null) {
                codecExtension = EnumHelper.GetFileExtension(vcodec.Value);
            } else {
                if (acodec != null) {
                    codecExtension = EnumHelper.GetFileExtension(acodec.Value);
                }
            }
            if (codecExtension != null) {
                name += codecExtension;
            } else {
                if (containerExtension != null) {
                    name += containerExtension;
                }
            }
            return name;
        }

        private static string GetImageExtension(ImageFormat format) {
            string? name = string.Empty;
            name = EnumHelper.GetFileExtension(format);
            if(name == null) {
                name = format.ToString();
            }
            return name;
        }
    }
}
