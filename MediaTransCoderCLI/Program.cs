using MediaTransCoder.Backend;
using MediaTransCoder.Shared;
using MediaTransCoder.Tests;

namespace MediaTransCoder.CLI {
    internal class Program {
        private static Endpoint? Backend;
        private static CLIConfig? Config;
        private static CLIDisplay GUI = CLIDisplay.GetInstance();

        static void Main(string[] args) {
            Setup();
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            Config = CLIConfig.ReadConfig();
            if(Config == null) {
                throw new Exception("Obtained config was null!");
            }
            GUI.Progress = new ProgressBar();
            Backend = new Endpoint(Config.Backend, GUI);

            var testDisplay = TestDisplay.GetInstance();
            testDisplay.Init(GUI);

            DateTime startTime = DateTime.Now;
            DateTime? endTime = null;
            GUI.Send("Starting tests:\n\t" + startTime.ToString("HH:mm:ss"), MessageType.DEBUG);
            //InformationTesting.TestExtensionsGeneration();
            //InformationTesting.GetCompatibilityLists();
            //InformationTesting.GetExtensions();
            //CompatibilityTests.TestCompatibilityInfo();
            //CompatibilityTests.TestCodecResolutionCompatibility();
            //CompatibilityTests.TestCompatiblityCharts();
            //ImageTests.TestJPGCompression();
            //ImageTests.TestFormats();
            //ImageTests.TestEffects();
            ConversionMeasuresTest.Measure();
            //ConvertVideo();
            //ConvertAudio();
            //ConvertImage();
            endTime = DateTime.Now;
            if (endTime.HasValue && endTime != null) {
                GUI.Send("\nTests finished:\n\t" + endTime?.ToString("HH:mm:ss"), MessageType.DEBUG);
                TimeSpan ts = (endTime ?? DateTime.Now) - startTime;
                GUI.Send("Duration:\t" + ts.ToString(), MessageType.DEBUG);
                GUI.Send("(" + startTime.ToString("HH:mm:ss") + " - " + endTime?.ToString("hh:mm:ss"), MessageType.DEBUG);
            }
        }

        #region Conversion
        private static void ConvertVideo(bool verbose = false) {
            var options = EndpointOptions.GetSampleVideoOptions();
            if (Backend != null) {
                Backend.IsDebug = verbose;
                Backend.ConvertVideo(options);
                Backend.IsDebug ^= verbose;
            } 
        }

        private static void ConvertAudio(bool verbose = false) {
            var options = EndpointOptions.GetSampleAudioOptions();
            if (Backend != null) {
                Backend.IsDebug = verbose;
                Backend.ConvertAudio(options);
                Backend.IsDebug ^= verbose;
            }
        }

        private static void ConvertImage(bool verbose = false) {
            var options = EndpointOptions.GetSampleImageOptions();
            if(Backend != null) {
                Backend.IsDebug = verbose;
                Backend.ConvertImage(options);
                Backend.IsDebug ^= verbose;
            }
        }
        #endregion

        private static void Setup() {
            var cfg = CLIConfig.Defaults();
            var env = EnvironmentalSettings.Get();
            cfg.Backend.Environment = EnvironmentType.Development;
            cfg.Backend.TempDirPath = env.RootPath + ".temp//";
            cfg.SaveConfig(env.ConfigPath + "config.json");
            TestingEnvironment.RootPath = @"E:\TEMP\mtc";
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs e) {
            e.Cancel = true;
            Backend?.Dispose();
            Environment.ExitCode = 0;
            Environment.Exit(0);
        }
    }
}