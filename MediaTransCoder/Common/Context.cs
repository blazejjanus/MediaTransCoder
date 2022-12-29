using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
namespace MediaTransCoder.Backend {
    internal class Context {
        internal BackendConfig Config { get; set; }
        internal FfmpegArgs? Args { get; set; }
        internal IDisplay Display { get; set; }
        private static Context? instance;

        private Context(BackendConfig config, IDisplay display) { 
            Config = config;
            Display = display;
        }

        internal static Context Get() {
            if (instance == null) {
                throw new Exception("Cannot get uninitialized context!");
            }
            return instance;
        }

        internal static void Init(BackendConfig config, IDisplay display, FfmpegArgs? args = null) {
            if (instance != null) {
                throw new Exception("Cannot overwrite existing context!");
            }
            instance = new Context(config, display);
            instance.Args = args;
        }

        public static bool IsSet {
            get {
                if(instance == null) {
                    return false;
                } else {
                    return true;
                }
            }
        }
    }
}
