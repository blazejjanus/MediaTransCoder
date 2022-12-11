namespace MediaTransCoder.Backend
{
    public class Endpoint
    {
        #region Constructor
        public Endpoint(BackendConfig config, IDisplay gui)
        {
            if (config == null)
            {
                throw new ArgumentNullException("Provided config was null!");
            }
            if (gui == null)
            {
                throw new ArgumentNullException("Provided gui was null!");
            }
            Context.Init(config, gui);
            context = Context.Get();
            context.Display = gui;
            instance = this;
        }
        #region Methods
        public void ConvertVideo()
        {

        }
        public void ConvertAudio()
        {

        }
        public void ConvertImage()
        {

        }
        #endregion
        #endregion
        #region Fields
        private Context context;
        protected static Endpoint? instance;
        #endregion
    }
}