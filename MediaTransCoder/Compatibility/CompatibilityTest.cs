using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend
{
    /// <summary>
    /// Test purpose only! Class used to test format - codecs compatibility chart
    /// </summary>
    internal static class CompatibilityTest {
        /// <summary>
        /// Test audio codecs compatibility with containers
        /// </summary>
        /// <param name="inputPath">Input file path</param>
        /// <param name="outputPath">[Output directory path.] If not provided output files will be saved in created subfolder of input path.</param>
        /// <returns>List of compatibility charts for all containers.</returns>
        internal static List<CompatibilityChart> Audio(string inputPath, string? outputPath = null) {
            outputPath = PrepeareOutput(inputPath, outputPath);
            var result = new List<CompatibilityChart>();
            var audioCodecs = Enum.GetValues(typeof(AudioCodecs)).Cast<AudioCodecs>().ToList();
            Context context = Context.Get();
            context.Display.Send("Attempting to test audio codecs compatibility...");
            foreach (ContainerFormat container in Enum.GetValues(typeof(ContainerFormat))) {
                context.Display.Send("Testing " + EnumHelper.GetName(container));
                Directory.CreateDirectory(outputPath + EnumHelper.GetCommand(container));
                var supportedAudioCodecs = new List<AudioCodecs>();
                for(int i = 0; i < audioCodecs.Count; i++) {
                    FfmpegArgs ffmpegArgs = new FfmpegArgs();
                    ffmpegArgs.Files.Input = inputPath;
                    ffmpegArgs.Files.Output = outputPath;
                    ffmpegArgs.Format = container;
                    ffmpegArgs.Audio = new AudioOptions();
                    ffmpegArgs.Audio.Codec = audioCodecs.ElementAt(i);
                    using(var caller = new FfmpegCaller(ffmpegArgs)) {
                        if (caller.Test()) {
                            supportedAudioCodecs.Add(audioCodecs.ElementAt(i));
                        }
                    }
                }
                result.Add(new CompatibilityChart(container) { AudioCodecs = supportedAudioCodecs });
            }
            return result;
        }

        /// <summary>
        /// Test video codecs compatibility with containers
        /// </summary>
        /// <param name="inputPath">Input file path</param>
        /// <param name="outputPath">[Output directory path.] If not provided output files will be saved in created subfolder of input path.</param>
        /// <returns>List of compatibility charts for all containers.</returns>
        internal static List<CompatibilityChart> Video(string inputPath, string? outputPath = null) {
            outputPath = PrepeareOutput(inputPath, outputPath);
            var result = new List<CompatibilityChart>();
            var videoCodecs = Enum.GetValues(typeof(VideoCodecs)).Cast<VideoCodecs>().ToList();
            Context context = Context.Get();
            context.Display.Send("Attempting to test video codecs compatibility...");
            foreach (ContainerFormat container in Enum.GetValues(typeof(ContainerFormat))) {
                context.Display.Send("Testing " + EnumHelper.GetName(container));
                Directory.CreateDirectory(outputPath + EnumHelper.GetCommand(container));
                var supportedVideoCodecs = new List<VideoCodecs>();
                for (int i = 0; i < videoCodecs.Count; i++) {
                    FfmpegArgs ffmpegArgs = new FfmpegArgs();
                    ffmpegArgs.Files.Input = inputPath;
                    ffmpegArgs.Files.Output = outputPath;
                    ffmpegArgs.Format = container;
                    ffmpegArgs.Video = new VideoOptions();
                    ffmpegArgs.Video.Codec = videoCodecs.ElementAt(i);
                    using (var caller = new FfmpegCaller(ffmpegArgs)) {
                        if (caller.Test()) {
                            supportedVideoCodecs.Add(videoCodecs.ElementAt(i));
                        }
                    }
                }
                result.Add(new CompatibilityChart(container) { VideoCodecs = supportedVideoCodecs });
            }
            return result;
        }

        /// <summary>
        /// Test audio and video codecs compatibility with containers
        /// </summary>
        /// <param name="inputPath">Input file path</param>
        /// <param name="outputPath">[Output directory path.] If not provided output files will be saved in created subfolder of input path.</param>
        /// <returns>List of compatibility charts for all containers.</returns>
        internal static List<CompatibilityChart> AudioVideo(string inputPath, string? outputPath = null) {
            outputPath = PrepeareOutput(inputPath, outputPath);
            var result = new List<CompatibilityChart>();
            var audioCodecs = Enum.GetValues(typeof(AudioCodecs)).Cast<AudioCodecs>().ToList();
            var videoCodecs = Enum.GetValues(typeof(VideoCodecs)).Cast<VideoCodecs>().ToList();
            Context context = Context.Get();
            context.Display.Send("Attempting to test audio and video codecs compatibility...");
            foreach (ContainerFormat container in Enum.GetValues(typeof(ContainerFormat))) {
                context.Display.Send("Testing " + EnumHelper.GetName(container));
                Directory.CreateDirectory(outputPath + "\\" + EnumHelper.GetCommand(container));
                var supportedVideoCodecs = new List<VideoCodecs>();
                var supportedAudioCodecs = new List<AudioCodecs>();
                for (int i = 0; i < audioCodecs.Count; i++) {
                    FfmpegArgs ffmpegArgs = new FfmpegArgs();
                    ffmpegArgs.Format = container;
                    ffmpegArgs.Audio = new AudioOptions();
                    ffmpegArgs.Video = new VideoOptions();
                    ffmpegArgs.Audio.Codec = audioCodecs.ElementAt(i);
                    ffmpegArgs.Video.Codec = VideoCodecs.hevc;
                    ffmpegArgs.Files.Input = inputPath;
                    ffmpegArgs.Files.Output = ffmpegArgs.Files.Output = outputPath + EnumHelper.GetCommand(container) + "\\output_" +
                            EnumHelper.GetName(ffmpegArgs.Video.Codec) + "_" +
                            EnumHelper.GetName(ffmpegArgs.Audio.Codec) + "." +
                            EnumHelper.GetCommand(container);
                    using (var caller = new FfmpegCaller(ffmpegArgs)) {
                        if (caller.Test()) {
                            supportedAudioCodecs.Add(audioCodecs.ElementAt(i));
                        }
                    }
                }
                for (int i = 0; i < videoCodecs.Count; i++) {
                    FfmpegArgs ffmpegArgs = new FfmpegArgs();
                    ffmpegArgs.Format = container;
                    ffmpegArgs.Audio = new AudioOptions();
                    ffmpegArgs.Video = new VideoOptions();
                    ffmpegArgs.Audio.Codec = AudioCodecs.mp3;
                    ffmpegArgs.Video.Codec = videoCodecs.ElementAt(i);
                    ffmpegArgs.Files.Input = inputPath;
                    ffmpegArgs.Files.Output = ffmpegArgs.Files.Output = outputPath + EnumHelper.GetCommand(container) + "\\output_" +
                            EnumHelper.GetName(ffmpegArgs.Video.Codec) + "_" +
                            EnumHelper.GetName(ffmpegArgs.Audio.Codec) + "." +
                            EnumHelper.GetCommand(container);
                    using (var caller = new FfmpegCaller(ffmpegArgs)) {
                        if (caller.Test()) {
                            supportedVideoCodecs.Add(videoCodecs.ElementAt(i));
                        }
                    }
                }
                result.Add(new CompatibilityChart(container) { 
                    VideoCodecs = supportedVideoCodecs, 
                    AudioCodecs = supportedAudioCodecs,
                });
            }
            return result;
        }

        private static string PrepeareOutput(string inputPath, string? outputPath) {
            if (outputPath == null)
                outputPath = Path.GetDirectoryName(inputPath) + "\\output\\";
            Directory.CreateDirectory(outputPath);
            return outputPath;
        }
    }
}
