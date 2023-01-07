namespace MediaTransCoder.WPF {
    internal class WPFContext {
        private static WPFContext? instance;

        private WPFContext() { }

        public static WPFContext Get() {
            return instance ?? (instance = new WPFContext());
        }
    }
}
