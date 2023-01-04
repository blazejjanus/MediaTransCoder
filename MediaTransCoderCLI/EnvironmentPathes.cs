﻿namespace MediaTransCoder.CLI {
    internal class EnvironmentPathes {
        public string RootDirectory { get; private set; }
        public string LogDirectory { get; private set; }
        private static EnvironmentPathes? instance;

        private EnvironmentPathes() {
            RootDirectory = Directory.GetCurrentDirectory();
            RootDirectory = RootDirectory.Split(".build").First();
            LogDirectory = Path.Combine(RootDirectory, ".logs");
            if (!Directory.Exists(LogDirectory)) {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        internal static EnvironmentPathes Get() {
            return instance ?? (instance = new EnvironmentPathes());
        }
    }
}
