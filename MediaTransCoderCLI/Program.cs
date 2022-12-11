using MediaTransCoder.Backend;
using System.Diagnostics;

namespace MediaTransCoder.CLI {
    internal class Program {
        //private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static IDisplay GUI = CLIDisplay.GetInstance();
        static void Main(string[] args) {
            string path = "E:\\TEMP\\mtc\\input\\sample2.mp4";
            //Setup();
            Config = CLIConfig.ReadConfig();
            if(Config== null) {
                throw new Exception("Obtained config was null!");
            }
            if(Debugger.IsAttached) {
                TestEndpoint backend = new TestEndpoint(Config.Backend, GUI);
                var charts = backend.TestAudioVideo(path);
                foreach(var chart in charts) {
                    Console.WriteLine("");
                    Console.WriteLine(chart.ToString());
                }
            } else {
                Endpoint backend = new Endpoint(Config.Backend, GUI);
            }
        }
        private static void Setup() {
            var cfg = CLIConfig.Defaults();
            var env = EnvironmentalSettings.Get();
            cfg.Backend.Environment = EnvironmentType.Development;
            cfg.Backend.TempDirPath = env.RootPath + ".temp//";
            cfg.Backend.Logging.LogFilePath = env.LogPath;
            cfg.Backend.Logging.LoggingLevel = LoggingLevel.INFO;
            cfg.SaveConfig(env.ConfigPath + "config.json");
        }
    }
}