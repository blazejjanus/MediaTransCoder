using MediaTransCoder.Backend;

namespace MediaTransCoder.WPF {
    internal class WPFContext {
        public HardwareAcceleration DefaultAcceleration { get; set; }
        public Endpoint? Backend { get; set; }
        public WPFDisplay? Display { get; set; }
        public WPFConfig? Config { get; set; }
        private static WPFContext? instance;

        private WPFContext() { }

        public static WPFContext Get() {
            return instance ?? (instance = new WPFContext());
        }
    }
}
