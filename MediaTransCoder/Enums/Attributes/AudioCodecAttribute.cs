namespace MediaTransCoder.Backend {
    public class AudioCodecAttribute : Attribute {
        /// <summary>
        /// Codec name - for ffmpeg
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Codec displayed name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// File extension associated with codec or null if no extension associated
        /// </summary>
        public string? FileExtension { get; set; }

        public AudioCodecAttribute(string name, string description, string? fileExtension = null) {
            Name = name;
            Description = description;
            FileExtension = fileExtension;
        }
    }
}
