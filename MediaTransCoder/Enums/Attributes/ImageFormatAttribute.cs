namespace MediaTransCoder.Backend {
    public class ImageFormatAttribute : Attribute {
        /// <summary>
        /// Image format name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ffmpeg image codec name associated with this format
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// File extensions associated with this format
        /// </summary>
        public List<string> Extensions { get; set; }

        public ImageFormatAttribute(string name) {
            Name = name;
            Command = name;
            Extensions = new List<string>();
            Extensions.Add("." + name);
        }

        public ImageFormatAttribute(string name, string command) {
            Name = name; 
            Command = command;
            Extensions = new List<string>();
            Extensions.Add("." + name);
        }

        public ImageFormatAttribute(string name, string[] extensions) { 
            Name = name;
            Command = name;
            Extensions = extensions.ToList();
        }

        public ImageFormatAttribute(string name, string command, string[] extensions) {
            Name = name;
            Command = command;
            Extensions = extensions.ToList();
        }
    }
}
