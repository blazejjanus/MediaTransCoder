namespace MediaTransCoder.Backend {
    public class ContainerFormatAttribute : Attribute {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? VideoExtension { get; set; }
        public string? AudioExtension { get; set; }
        public bool IsVideoFormat {
            get {
                if (VideoExtension != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        public bool IsAudioFormat {
            get {
                if (AudioExtension != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public ContainerFormatAttribute(string name, string? videoExtension, string? audioExtension) {
            Name = name;
            Description = name;
            VideoExtension = videoExtension;
            AudioExtension = audioExtension;
        }

        public ContainerFormatAttribute(string name, string description, string? videoExtension, string? audioExtension) {
            Name = name;
            Description = description;
            VideoExtension = videoExtension;
            AudioExtension = audioExtension;
        }
    }
}
