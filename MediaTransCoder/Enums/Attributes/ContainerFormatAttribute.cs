namespace MediaTransCoder.Backend {
    public class ContainerFormatAttribute : Attribute {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? VideoExtension { get; set; }
        public string? AudioExtension { get; set; }
        public bool AudioOnly { get; set; }

        public ContainerFormatAttribute() { 
            Name = string.Empty;
            Description = string.Empty;
        }

        public ContainerFormatAttribute(string name, string description, string videoExtension, string audioExtension) {
            Name = name;
            Description = description;
            AudioOnly = false;
            AudioExtension = audioExtension;
            VideoExtension = videoExtension;
        }

        public ContainerFormatAttribute(string name, string description, string extension, bool audioOnly = false) {
            Name = name;
            Description = description;
            AudioOnly = audioOnly;
            AudioExtension = extension;
            if (!AudioOnly) {
                VideoExtension = extension;
            }
        }

        public ContainerFormatAttribute(string name, string description, bool audioOnly = false) {
            Name = name;
            Description = description;
            AudioOnly = audioOnly;
        }

        public ContainerFormatAttribute(string name, bool audioOnly = false) {
            Name = name;
            Description = name;
            AudioOnly = audioOnly;
        }
    }
}
