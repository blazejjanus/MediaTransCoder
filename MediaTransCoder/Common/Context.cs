﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MediaTransCoder.Tests")]
[assembly: InternalsVisibleTo("MediaTransCoder.UnitTests")]
namespace MediaTransCoder.Backend {
    internal class Context {
        internal BackendConfig Config { get; set; }
        public bool? IsDebug { get; set; } = null;
        internal IDisplay Display { get; set; }
        private static Context? instance;

        private Context(BackendConfig config, IDisplay display, bool? debug = null) { 
            Config = config;
            Display = display;
            IsDebug = debug;
        }

        internal static Context Get() {
            if (instance == null) {
                throw new Exception("Cannot get uninitialized context!");
            }
            return instance;
        }

        internal static void Init(BackendConfig config, IDisplay display, bool? debug = null) {
            if (instance != null) {
                throw new Exception("Cannot overwrite existing context!");
            }
            instance = new Context(config, display, debug);
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
