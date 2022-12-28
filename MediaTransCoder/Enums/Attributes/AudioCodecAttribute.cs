namespace MediaTransCoder.Backend {
    public class AudioCodecAttribute : Attribute {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? FileExtension { get; set; }

        public AudioCodecAttribute(string name, string description, string? fileExtension = null) {
            Name = name;
            Description = description;
            FileExtension = fileExtension;
        }
    }
}
