using MediaTransCoder.Backend;
using System;

namespace MediaTransCoder.CLI {
    internal static class InformationTesting {
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static CLIDisplay GUI = CLIDisplay.GetInstance();

        internal static void GetExtensions(bool searchCriteria = false) {
            var audio = FileExtensions.GetAudioExtensions(searchCriteria);
            Console.WriteLine("##############################");
            Console.WriteLine("Audio Extensions:");
            foreach (var extension in audio) {
                Console.WriteLine("\t" + extension);
            }
            Console.WriteLine("##############################");
            var video = FileExtensions.GetVideoExtensions(searchCriteria);
            Console.WriteLine("Video Extensions:");
            foreach (var extension in video) {
                Console.WriteLine("\t" + extension);
            }
            Console.WriteLine("##############################");
        }

        private static void GetCompatibilityLists(bool file = false) {
            //TODO: Refactor to CLIDisplay.Log()
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            string content = string.Empty;
            foreach (ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
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
            Console.Write(content);
            if (file) {
                File.WriteAllText(Pathes.LogDirectory + "\\compatibility_info.txt", content);
            }
        }

        private static void TestExtensionsGeneration() {
            GUI.LogFile = Pathes.LogDirectory + "\\extensions.log";
            List<AudioCodecs> audioCodecs = new List<AudioCodecs>();
            List<VideoCodecs> videoCodecs = new List<VideoCodecs>();
            if (File.Exists(GUI.LogFile)) {
                if (File.Exists(GUI.LogFile + ".old")) {
                    File.Delete(GUI.LogFile + ".old");
                }
                File.Move(GUI.LogFile, GUI.LogFile + ".old");
            }
            //Test video
            GUI.Log("\nVideo:", MessageType.SUCCESS);
            foreach (ContainerFormat format in EnumHelper.GetVideoFormats()) {
                GUI.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                videoCodecs = CompatibilityInfo.GetCompatibleVideoCodecs(format);
                foreach (var vcodec in videoCodecs) {
                    foreach (var acodec in audioCodecs) {
                        string exc = FfmpegArgs.GenerateOutputFileExtension(format, vcodec, acodec);
                        GUI.Log("\t\t" + format + "_" + vcodec + "_" + acodec + ": " + exc);
                    }
                }
            }
            //Test audio
            GUI.Log("\nAudio:", MessageType.SUCCESS);
            var formats = EnumHelper.GetAudioFormats();
            foreach (ContainerFormat format in EnumHelper.GetAudioFormats()) {
                GUI.Log("\t" + format + ":", MessageType.SUCCESS);
                audioCodecs = CompatibilityInfo.GetCompatibleAudioCodecs(format);
                foreach (var acodec in audioCodecs) {
                    string exc = FfmpegArgs.GenerateOutputFileExtension(format, null, acodec, true);
                    GUI.Log("\t\t" + format + "_" + acodec + ": " + exc);
                }
            }
        }
    }
}
