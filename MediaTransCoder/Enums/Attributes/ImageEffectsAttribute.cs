namespace MediaTransCoder.Backend {
    public class ImageEffectsAttribute : Attribute {
        string Name { get; set; }
        string Command { get; set; }

        public ImageEffectsAttribute(string name, string command) {
            Name = name;
            Command = command;
        }
    }
}
