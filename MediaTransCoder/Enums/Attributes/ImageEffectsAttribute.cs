namespace MediaTransCoder.Backend {
    public class ImageEffectsAttribute : Attribute {
        public string Name { get; set; }
        public string Command { get; set; }

        public ImageEffectsAttribute(string name, string command) {
            Name = name;
            Command = command;
        }
    }
}
