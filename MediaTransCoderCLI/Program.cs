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

            //InformationTesting.TestExtensionsGeneration();
            //InformationTesting.GetCompatibilityLists();
            //InformationTesting.GetExtensions();
            //CompatibilityTests.TestCompatibilityInfo();
            //CompatibilityTests.TestCodecResolutionCompatibility();
            //CompatibilityTests.TestCompatiblityCharts();
            //ImageTests.TestJPGCompression();
            //ImageTests.TestFormats();
            ImageTests.TestEffects(true);
            //ConvertVideo();
            //ConvertAudio();
            //ConvertImage();
        }

        #region Conversion
        private static void ConvertVideo() {
            var options = EndpointOptions.GetSampleVideoOptions();
            Backend?.ConvertVideo(options);
        }

        private static void ConvertAudio() {
            var options = EndpointOptions.GetSampleAudioOptions();
            Backend?.ConvertAudio(options);
        }

        private static void ConvertImage() {
            var options = EndpointOptions.GetSampleImageOptions();
            Backend?.ConvertImage(options);
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