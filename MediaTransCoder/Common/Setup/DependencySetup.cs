using System.Diagnostics;

namespace MediaTransCoder.Backend {
    internal static class DependencySetup {
        public static bool CheckEnvironmentalPaths() {
            return CheckFfmpeg("ffmpeg");
        }

        public static bool CheckFfmpegPath(string path) {
            return CheckFfmpeg(path);
        }

        private static bool CheckFfmpeg(string path) {
            try {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo() {
                    UseShellExecute = false,
                    FileName = "ffmpeg",
                    Arguments = "-version"
                };
                process.Start();
                process.WaitForExit();
                return true;
            } catch (Exception exc) {
                Logging.Debug(exc.Message);
                return false;
            }
        }
    }
}