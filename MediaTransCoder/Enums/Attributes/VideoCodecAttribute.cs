namespace MediaTransCoder.Backend {
    public class VideoCodecAttribute : Attribute {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }

        public VideoCodecAttribute(string name, string description, string fileExtension) {
            Name = name;
            Description = description;
            FileExtension = fileExtension;
        }
    }
}
