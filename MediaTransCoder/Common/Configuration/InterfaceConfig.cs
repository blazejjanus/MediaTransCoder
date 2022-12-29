using System.Diagnostics;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Configuration connected to interface calling backend
    /// </summary>
    public class InterfaceConfig {
        /// <summary>
        /// Represents type of interface calling backend, eg. CLI, GUI
        /// </summary>
        public InterfaceType Type { get; set; }
        /// <summary>
        /// Determines if backend can access console directly
        /// </summary>
        public bool UseConsole { get; set; }
        /// <summary>
        /// Determines if backend should write to debug console
        /// </summary>
        public bool UseDebug { get; set; }

        public InterfaceConfig() {
            Type = InterfaceType.CLI;
            UseConsole = false;
            UseDebug = false;
        }
    }
}
