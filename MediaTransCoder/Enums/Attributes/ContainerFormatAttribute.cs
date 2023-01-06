namespace MediaTransCoder.Backend {
    public class ContainerFormatAttribute : Attribute {
        /// <summary>
        /// Container name - for ffmpeg
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Container displayed name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// File extension associated with container for video files, null if no extension associated
        /// </summary>
        public string? VideoExtension { get; set; }
        /// <summary>
        /// File extension associated with container for audio files, null if no extension associated
        /// </summary>
        public string? AudioExtension { get; set; }
        /// <summary>
        /// Determines if container supports video
        /// </summary>
        public bool IsVideoFormat {
            get {
                if (VideoExtension != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        /// <summary>
        /// Determines if container supports audio
        /// </summary>
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
