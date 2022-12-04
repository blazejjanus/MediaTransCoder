namespace MediaTransCoder.Backend {
    public class Main {
        /// <summary>
        /// Call this method to initialize library
        /// </summary>
        /// <param name="config"></param>
        /// <param name="gui"></param>
        public void Init(IConfig config, IDisplay gui) {
            if (config == null) {
                throw new ArgumentNullException("Provided config was null!");
            }
            if (gui == null) {
                throw new ArgumentNullException("Provided gui was null!");
            }
            this.Config = config;
            this.GUI = gui;
        }
        internal IConfig Config {
            get;
            private set;
        }
        internal IDisplay GUI {
            get;
            private set;
        }
    }
}