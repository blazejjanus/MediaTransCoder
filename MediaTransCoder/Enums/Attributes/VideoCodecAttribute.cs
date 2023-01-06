namespace MediaTransCoder.Backend {
    public class VideoCodecAttribute : Attribute {
        /// <summary>
        /// Ffmpeg codec name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Codec displayed name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// File extension associated with codec or null if no such extension
        /// </summary>
        public string? FileExtension { get; set; }

        public VideoCodecAttribute(string name, string description, string? fileExtension = null) {
            Name = name;
            Description = description;
            FileExtension = fileExtension;
        }
    }
}
