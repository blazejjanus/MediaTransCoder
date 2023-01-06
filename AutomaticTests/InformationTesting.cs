using MediaTransCoder.Backend;
using MediaTransCoder.Shared;
using System.Text;

namespace MediaTransCoder.Tests {
    public static class InformationTesting {
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static TestDisplay Display = TestDisplay.GetInstance();

        public static void GetExtensions(bool searchCriteria = false) {
            Display.LogFile = Pathes.LogDirectory + "\\extension_list.log";
            var audio = FileExtensions.GetAudioExtensions(searchCriteria);
            var sb = new StringBuilder();
            sb.AppendLine("##############################");
            sb.AppendLine("Audio Extensions:");
            foreach (var extension in audio) {
                sb.AppendLine("\t" + extension);
            }
            sb.AppendLine("##############################");
            var video = FileExtensions.GetVideoExtensions(searchCriteria);
            sb.AppendLine("Video Extensions:");
            foreach (var extension in video) {
                sb.AppendLine("\t" + extension);
            }
            sb.AppendLine("##############################");
            var image = FileExtensions.GetImageExtensions(searchCriteria);
            sb.AppendLine("Image Extensions:");
            foreach (var extension in image) {
                sb.AppendLine("\t" + extension);
            }
            sb.AppendLine("##############################");
            Display.Log(sb.ToString());
        }

        public static void GetCompatibilityLists() {
            Display.LogFile = Pathes.LogDirectory + "\\compatibility_info.log";
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            string content = string.Empty;
            foreach (ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
                videoCodecs = Compatibility.GetCompatibleVideoCodecs(format);
                content += EnumHelper.GetName(format) + ":\n";
                content += "\tAudio:\n";
                foreach (var codec in audioCodecs) {
                    content += "\t\t" + EnumHelper.GetName(codec) + "\n";
                }
                content += "\tVideo:\n";
                foreach (var codec in videoCodecs) {
                    content += "\t\t" + EnumHelper.GetName(codec) + "\n";
                }
                content += "##############################\n\n";
            }
            Display.Log(content);
        }

        public static void TestExtensionsGeneration() {
            Display.LogFile = Pathes.LogDirectory + "\\extensions.log";
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            //TestCompatiblityCharts video
            Display.Log("\nVideo:", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                Display.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
                videoCodecs = Compatibility.GetCompatibleVideoCodecs(format);
                foreach (var vcodec in videoCodecs) {
                    foreach (var acodec in audioCodecs) {
                        string exc = FfmpegArgs.GenerateOutputFileExtension(format, null, vcodec, acodec);
                        Display.Log("\t\t" + format + "_" + vcodec + "_" + acodec + ": " + exc);
                    }
                }
            }
            //TestCompatiblityCharts audio
            Display.Log("\nAudio:", MessageType.SUCCESS);
            var formats = EnumHelper.GetAudioFormats();
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                Display.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = Compatibility.GetCompatibleAudioCodecs(format);
                foreach (var acodec in audioCodecs) {
                    string exc = FfmpegArgs.GenerateOutputFileExtension(format, null, null, acodec, true);
                    Display.Log("\t\t" + format + "_" + acodec + ": " + exc);
                }
            }
        }
    }
}
