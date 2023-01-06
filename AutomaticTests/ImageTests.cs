using MediaTransCoder.Backend;
using MediaTransCoder.Shared;

namespace MediaTransCoder.Tests {
    public static class ImageTests {
        private static Endpoint? Backend;
        private static BackendConfig? Config;
        private static EnvironmentPathes Pathes = EnvironmentPathes.Get();
        private static TestDisplay Display = TestDisplay.GetInstance();

        static ImageTests() {
            Config = new BackendConfig();
            Config.Hardware.GPU = GPUType.NONE;
            Config.Hardware.CPUCores = 16;
            Config.Environment = EnvironmentType.Test;
            Backend = new Endpoint(Config, Display);
            Display.ShowProgress = false;
        }

        public static void TestFormats(EndpointOptions? options = null, bool verbose = false) {
            if(options == null) {
                options = EndpointOptions.GetSampleImageOptions();
            }
            if (options.Image == null) {
                throw new NullReferenceException();
            }
            Display.LogFile = Pathes.LogDirectory + "\\image_conversion.log";
            TryFfmpeg? caller = null;
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            foreach(ImageFormat format in Enum.GetValues(typeof(ImageFormat))) {
                Display.Log(format + ": ");
                options.Image.Format = format;
                options.OutputFileName = format.ToString();
                caller?.Image(options);
            }
        }

        public static void TestJPGCompression(bool verbose = false) {
            var options = EndpointOptions.GetSampleImageOptions();
            if(options.Image == null) {
                throw new NullReferenceException();
            }
            TryFfmpeg? caller = null;
            Display.LogFile = Pathes.LogDirectory + "\\jpg_conversion.log";
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            options.Image.Format = ImageFormat.JPG;
            Display.Log("Testing JPG conversion:");
            for(int i = 1; i <= 31; i++) {
                options.Image.CompressionLevel = i;
                options.OutputFileName = "jpg_" + i;
                caller?.Image(options);
            }
            options.Image.Format = ImageFormat.JPG2000;
            Display.Log("Testing JPG-2000 conversion:");
            for (int i = 1; i <= 31; i++) {
                options.Image.CompressionLevel = i;
                options.OutputFileName = "jpg2000_" + i;
                caller?.Image(options);
            }
        }
        public static void TestEffects(bool verbose = false) {
            var options = EndpointOptions.GetSampleImageOptions();
            if (options.Image == null) {
                throw new NullReferenceException();
            }
            TryFfmpeg? caller = null;
            Display.LogFile = Pathes.LogDirectory + "\\image_effects.log";
            if (Backend != null) {
                Backend.IsDebug = verbose;
                caller = new TryFfmpeg(Backend);
                caller.Verbose = verbose;
            }
            Display.Log("Testing effects:");
            string name = string.Empty;
            string originalName = name;
            //Brightness
            options.Image.Brightness = 50;
            name = originalName + "_brightness_inc";
            options.OutputFileName = name;
            Display.Log(originalName + " brightness +50%: ");
            caller?.Image(options);
            options.Image.Brightness = -50;
            name = originalName + "_brightness_dec";
            options.OutputFileName = name;
            Display.Log(originalName + " brightness -50%: ");
            caller?.Image(options);
            options.Image.Brightness = null;
            //Contrast
            options.Image.Contrast = 50;
            name = originalName + "_contrast_inc";
            options.OutputFileName = name;
            Display.Log(originalName + " contrast +50%: ");
            caller?.Image(options);
            options.Image.Contrast = -50;
            name = originalName + "_contrast_dec";
            options.OutputFileName = name;
            Display.Log(originalName + " contrast -50%: ");
            caller?.Image(options);
            options.Image.Contrast = null;
            //Saturation
            options.Image.Saturation = 50;
            name = originalName + "_saturation_inc";
            options.OutputFileName = name;
            Display.Log(originalName + " saturation +50%: ");
            caller?.Image(options);
            options.Image.Saturation = -50;
            name = originalName + "_saturation_dec";
            options.OutputFileName = name;
            Display.Log(originalName + " saturation -50%: ");
            caller?.Image(options);
            options.Image.Saturation = null;
            foreach (ImageEffects effect in Enum.GetValues(typeof(ImageEffects))) {
                name = EnumHelper.GetName(effect);
                originalName = name;
                Display.Log(name + ": ");
                options.OutputFileName = name;
                caller?.Image(options);
                //Brightness
                options.Image.Brightness = 50;
                name = originalName + "_brightness_inc";
                options.OutputFileName = name;
                Display.Log(originalName + " brightness +50%: ");
                caller?.Image(options);
                options.Image.Brightness = -50;
                name = originalName + "_brightness_dec";
                options.OutputFileName = name;
                Display.Log(originalName + " brightness -50%: ");
                caller?.Image(options);
                options.Image.Brightness = null;
                //Contrast
                options.Image.Contrast = 50;
                name = originalName + "_contrast_inc";
                options.OutputFileName = name;
                Display.Log(originalName + " contrast +50%: ");
                caller?.Image(options);
                options.Image.Contrast = -50;
                name = originalName + "_contrast_dec";
                options.OutputFileName = name;
                Display.Log(originalName + " contrast -50%: ");
                caller?.Image(options);
                options.Image.Contrast = null;
                //Saturation
                options.Image.Saturation = 50;
                name = originalName + "_saturation_inc";
                options.OutputFileName = name;
                Display.Log(originalName + " saturation +50%: ");
                caller?.Image(options);
                options.Image.Saturation = -50;
                name = originalName + "_saturation_dec";
                options.OutputFileName = name;
                Display.Log(originalName + " saturation -50%: ");
                caller?.Image(options);
                options.Image.Saturation = null;
            }
        }
    }
}
