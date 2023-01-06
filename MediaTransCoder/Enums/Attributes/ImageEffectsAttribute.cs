namespace MediaTransCoder.Backend {
    public class ImageEffectsAttribute : Attribute {
        /// <summary>
        /// Displayed name of effect
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ffmpeg command or setting to achieve effect
        /// </summary>
        public string Command { get; set; }

        public ImageEffectsAttribute(string name, string command) {
            Name = name;
            Command = command;
        }
    }
}
